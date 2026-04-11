namespace PropertyMgmt.Application.Features.Listings.Query.GetAllListings;

public record ListingLookupDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string AddressSummary { get; init; } = string.Empty; // دمج المدينة والشارع مثلاً
    public decimal PricePerNight { get; init; }
    public string TypeName { get; init; } = string.Empty;
    public string MainImageUrl { get; init; } = string.Empty; // جلب الصورة الأولى فقط
}
