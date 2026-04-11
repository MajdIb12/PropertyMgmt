using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Application.Common.Exceptions;

namespace PropertyMgmt.Application.Features.Listings.Query.GetListingById;

public class GetListingByIdQueryHandler : IRequestHandler<GetListingByIdQuery, ListingDto>
{
    private readonly IApplicationDbContext _context;
    private readonly GetListingByIdMapper _mapper; // سنستخدم الماپر هنا أيضاً

    public GetListingByIdQueryHandler(IApplicationDbContext context, GetListingByIdMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ListingDto> Handle(GetListingByIdQuery request, CancellationToken cancellationToken)
    {
        
        var listingDto = await _mapper.ProjectToDto(_context.Listings.AsNoTracking())
    .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken)
    ?? throw new NotFoundException("Listing", request.Id);

        return listingDto;
    }
}