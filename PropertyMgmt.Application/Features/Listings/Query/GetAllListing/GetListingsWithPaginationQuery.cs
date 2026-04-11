using MediatR;

namespace PropertyMgmt.Application.Features.Listings.Query.GetAllListings;

public record GetListingsWithPaginationQuery : IRequest<PaginatedList<ListingLookupDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
