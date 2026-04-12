using PropertyMgmt.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace PropertyMgmt.Application.Features.Listings.Query.GetAllListings;

[Mapper]
public partial class GetListingsWithPaginationMapper
{
    [MapProperty("ListingType.Name", "TypeName")]
    public partial ListingLookupDto MapToListingLookupDto(Listing listing);

    
    private string MapAddressToSummary(Listing listing)
    {
        
        return $"{listing.Address.City}, {listing.Address.Street}";
    }

    private string MapToMainImage(Listing listing)
    {
        return listing.Images.Where(i => i.IsPrimary).Select(i => i.ImageUrl).FirstOrDefault() ?? "default-image.jpg";
    }
}