using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.ListingTypes.Query.GetListingTypeById;

public class GetListingTypeByIdQueryHandler : IRequestHandler<GetListingTypeByIdQuery, ListingTypeDto>
{
    private readonly IApplicationDbContext _context;

    public GetListingTypeByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ListingTypeDto> Handle(GetListingTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var listingType = await _context.ListingTypes
        .AsNoTracking()
        .FirstOrDefaultAsync(lt => lt.Id == request.Id, cancellationToken)
        ?? throw new NotFoundException(nameof(ListingTypes), request.Id);

        var listingTypeDto = ListingTypeDto.Create(listingType.Id, listingType.Name, listingType.Description);

        return listingTypeDto;
    }
}