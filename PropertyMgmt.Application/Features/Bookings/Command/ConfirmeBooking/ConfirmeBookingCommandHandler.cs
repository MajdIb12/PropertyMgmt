using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Bookings.Command.ConfirmeBooking;

public class ConfirmeBookingCommandHandler(IApplicationDbContext context) : IRequestHandler<ConfirmeBookingCommand, bool>
    {
        public async Task<bool> Handle(ConfirmeBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await context.Bookings.FindAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Bookings), request.Id);
            if (!booking.TryConfirm())
                throw new ConfirmFailedException();

            var listing = await context.Listings.FirstOrDefaultAsync(l => l.Id == booking.ListingId)
                ?? throw new NotFoundException(nameof(Listings));
            if (!listing.TryRent())
                throw new ConfirmFailedException();

            var payment = context.Payments.FirstOrDefault(p => p.BookingId == booking.Id)
                ?? throw new NotFoundException(nameof(Payment));

            if (payment.TryComplete())
                throw new ConfirmFailedException();

            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
