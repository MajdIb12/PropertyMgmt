using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Features.Notifications.BookingRequest;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;
using PropertyMgmt.Domain.Enums;

namespace PropertyMgmt.Application.Features.Bookings.Command.CreateBooking;

public class CreateBookingCommandHandler(IApplicationDbContext context, IMediator mediator) : IRequestHandler<CreateBookingCommand, Guid>
{
    public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var listing = await context.Listings.FirstOrDefaultAsync(l => l.Id == request.ListingId && l.Status == ListingStatus.Available, cancellationToken)
        ?? throw new BookingConflictException();

        var user = await context.Customers
             .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken)
             ?? throw new NotFoundException(nameof(User), request.UserId);

        var booking = new Booking(request.ListingId, request.UserId, request.StartDate, request.EndDate, request.TotalPrice);
       
        if (!listing.TryReserve())
        {
            throw new BookingConflictException();
        }

        await context.Bookings.AddAsync(booking);
        var payment = new Payment
        {
            BookingId = booking.Id,
            Method = request.PaymentMethod,
            Currency = request.currencyCode,
            Status = PaymentStatus.Pending,
            Amount = request.TotalPrice
        };
        
        await context.Payments.AddAsync(payment);
        await context.SaveChangesAsync(cancellationToken);

        await mediator.Publish(new BookingRequestedEvent(booking.Id, listing.OwnerId, user.FullName, listing.Name, booking.TenantId));
        return booking.Id;
    }
}
