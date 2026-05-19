using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Bookings.Command.DeleteBooking;

public class DeleteBookingCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteBookingCommand, bool>
{
    public async Task<bool> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await context.Bookings.FindAsync(request.Id)
            ?? throw new NotFoundException(nameof(Booking), request.Id);

        var payment = await context.Payments.FirstOrDefaultAsync(p => p.BookingId == request.Id)
            ?? throw new NotFoundException(nameof(Payment), request.Id);

        var listing = await context.Listings.FirstOrDefaultAsync(l => l.Id == booking.Id)
            ?? throw new NotFoundException(nameof(Listing), booking.ListingId);

       listing.MakeAvailable();
        context.Bookings.Remove(booking);
        context.Payments.Remove(payment);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
