using PropertyMgmt.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace PropertyMgmt.Application.Features.Listings.Query.GetAllListings;

[Mapper]
public partial class GetListingsWithPaginationMapper
{
    [MapProperty("ListingType.Name", "TypeName")]
    public partial ListingLookupDto MapToListingLookupDto(Listing listing);

    // 2. تخصيص دمج العنوان وجلب الصورة الأولى
    private string MapAddressToSummary(Listing listing)
    {
        // دمج المدينة والشارع من الـ Value Object
        return $"{listing.Address.City}, {listing.Address.Street}";
    }

    private string MapToMainImage(Listing listing)
    {
        // جلب أول صورة من القائمة أو نص افتراضي
        return listing.Images.Where(i => i.IsPrimary).Select(i => i.ImageUrl).FirstOrDefault() ?? "default-image.jpg";
    }
}