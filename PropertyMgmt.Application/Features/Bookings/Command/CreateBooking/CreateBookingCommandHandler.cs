using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;
using PropertyMgmt.Domain.Enums;

namespace PropertyMgmt.Application.Features.Bookings.Command.CreateBooking;

public class CreateBookingCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateBookingCommand, Guid>
{
    public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var Listing = await context.Listings.FirstOrDefaultAsync(l => l.Id == request.ListingId && l.Status == ListingStatus.Available, cancellationToken)
        ?? throw new BookingConflictException();

        var Userexists = await context.Customers.AnyAsync(u => u.Id == request.UserId, cancellationToken);
        if (!Userexists)
            throw new NotFoundException(nameof(User), request.UserId);

        var booking = new Booking(request.ListingId, request.UserId, request.StartDate, request.EndDate, request.TotalPrice);
       
        if (!Listing.TryReserve())
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
        return booking.Id;
    }
}
