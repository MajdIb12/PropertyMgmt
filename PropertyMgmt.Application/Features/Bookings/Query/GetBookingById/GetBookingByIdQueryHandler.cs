using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Bookings.Query.GetBookingById;

public class GetBookingByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetBookingByIdQuery, BookingDetailsDto>
{
    public async Task<BookingDetailsDto> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        var booking = await context.Bookings
            .AsNoTracking() 
            .Where(b => b.Id == request.Id)
            .Select(b => new BookingDetailsDto
            {
                BookingId = b.Id,
                TotalPrice = b.TotalPrice,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                Status = b.Status.ToString(),
                ListingId = b.ListingId,
                ListingTitle = b.Listing.Name,
                OwnerId = b.Listing.OwnerId,
                OwnerName = b.Listing.Owner.FullName
            })
            .FirstOrDefaultAsync(cancellationToken);

        return booking ?? throw new NotFoundException(nameof(Booking), request.Id);
    }
}
