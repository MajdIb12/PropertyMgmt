using Riok.Mapperly.Abstractions;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Listings.Commands.CreateListing;

[Mapper(
    RequiredMappingStrategy = RequiredMappingStrategy.None
)]
public partial class CreateListingMapper
{
    public partial Listing MapToEntity(CreateListingCommand command);
}