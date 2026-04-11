using Riok.Mapperly.Abstractions;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Listings.Commands.CreateListing;

[Mapper(
    RequiredMappingStrategy = RequiredMappingStrategy.None
)]
public partial class UpdateListingMapper
{
    public partial void MapToExistingEntity(UpdateListingCommand source, Listing target);
}