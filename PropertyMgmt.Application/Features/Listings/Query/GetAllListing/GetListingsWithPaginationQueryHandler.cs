using PropertyMgmt.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using PropertyMgmt.Application.Common.Model;

namespace PropertyMgmt.Application.Features.Listings.Query.GetAllListings;

public class GetListingsWithPaginationQueryHandler 
    : IRequestHandler<GetListingsWithPaginationQuery, PaginatedList<ListingLookupDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly GetListingsWithPaginationMapper _mapper; // Mapperly 

    public GetListingsWithPaginationQueryHandler(IApplicationDbContext context, GetListingsWithPaginationMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ListingLookupDto>> Handle(GetListingsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        
        var query = _context.Listings
            .AsNoTracking()
            .Include(x => x.ListingType)
            .Include(x => x.Images)
            .OrderBy(x => x.Name); 

        var count = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var dtos = items.Select(_mapper.MapToListingLookupDto).ToList();

        return new PaginatedList<ListingLookupDto>(dtos, count, request.PageNumber, request.PageSize);
    }
}
