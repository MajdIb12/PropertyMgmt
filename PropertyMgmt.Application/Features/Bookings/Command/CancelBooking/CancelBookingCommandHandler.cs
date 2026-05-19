using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Bookings.Command.CancelBooking;

public class CancelBookingCommandHandler(IApplicationDbContext context) : IRequestHandler<CancelBookingCommand, bool>
{
    public async Task<bool> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await context.Bookings.FirstOrDefaultAsync(b => b.Id == request.BookingId, cancellationToken)
            ?? throw new NotFoundException(nameof(Bookings), request.BookingId);

        if (!booking.TryCancel())
        {
            throw new CancelFailedException(request.BookingId);
        }

        var payment = await context.Payments.FirstOrDefaultAsync(p => p.BookingId == request.BookingId, cancellationToken);
        if (payment != null)
        {
            if (!payment.TryCancel())
            {
                throw new CancelFailedException(request.BookingId);
            }
        }

        var listing = await context.Listings.FirstOrDefaultAsync(l => l.Id == booking.ListingId, cancellationToken)
            ?? throw new NotFoundException(nameof(Listings), booking.ListingId);

        listing.MakeAvailable(); 

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
