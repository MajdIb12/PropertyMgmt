using PropertyMgmt.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace PropertyMgmt.Application.Features.Listings.Query.GetListingById;

[Mapper(
    RequiredMappingStrategy = RequiredMappingStrategy.None
)]
public partial class GetListingByIdMapper
{
    public partial IQueryable<ListingDto> ProjectToDto(IQueryable<Listing> query);
}