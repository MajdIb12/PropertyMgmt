using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Bookings.Query.GetAllbookingByOwnerId;

public class GetAllBookingByOwnerIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllBookingByOwnerIdQuery, PaginatedList<BookingListDto>>
{
    public async Task<PaginatedList<BookingListDto>> Handle(GetAllBookingByOwnerIdQuery request, CancellationToken cancellationToken)
    {
        var query = context.Bookings.AsNoTracking()
            .Where(b => b.Listing.OwnerId == request.OwnerId)
            .Select(b => new BookingListDto
            {
                BookingId = b.Id,
                Status = b.Status.ToString(),
                ListingId = b.ListingId,
                OwnerId = request.OwnerId
            });

        return await PaginatedList<BookingListDto>.CreateAsync(query, request.PageNumber, request.PageSize, cancellationToken);
    }
}
