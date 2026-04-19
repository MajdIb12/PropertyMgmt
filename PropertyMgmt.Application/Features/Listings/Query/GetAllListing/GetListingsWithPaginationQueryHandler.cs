using PropertyMgmt.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using PropertyMgmt.Application.Common.Model;

namespace PropertyMgmt.Application.Features.Listings.Query.GetAllListings;

public class GetListingsWithPaginationQueryHandler 
    : IRequestHandler<GetListingsWithPaginationQuery, PaginatedList<ListingLookupDto>>
{
    private readonly IApplicationDbContext _context;

    public GetListingsWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

   public async Task<PaginatedList<ListingLookupDto>> Handle(GetListingsWithPaginationQuery request, CancellationToken cancellationToken)
   {
    var query = _context.Listings
        .AsNoTracking()
        .OrderBy(x => x.Name)
        .Select(ListingLookupDto.Projection); // استدعاء المنطق المخزن

    return await PaginatedList<ListingLookupDto>.CreateAsync(
        query, 
        request.PageNumber, 
        request.PageSize, 
        cancellationToken);
    }

}
