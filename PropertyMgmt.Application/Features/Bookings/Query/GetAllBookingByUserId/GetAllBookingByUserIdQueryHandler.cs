using FluentValidation;
using MediatR;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Bookings.Query.GetAllBookingByUserId;

public class GetAllBookingByUserIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllBookingByUserIdQuery, PaginatedList<BookingListDto>>
{
    public async Task<PaginatedList<BookingListDto>> Handle(GetAllBookingByUserIdQuery request, CancellationToken cancellationToken)
    {
        var query = context.Bookings
            .Where(b => b.UserId == request.UserId)
            .Select(b => new BookingListDto
            {
                BookingId = b.Id,
                Status = b.Status.ToString(),
                ListingId = b.ListingId,
                OwnerId = b.Listing.OwnerId
            });
        return await PaginatedList<BookingListDto>.CreateAsync(query, request.PageNumber, request.PageSize, cancellationToken);
    }
}
