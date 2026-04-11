namespace PropertyMgmt.Application.Features.Listings.Query.GetListingById;
public record ListingDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    
    // Address Flattening
    public string Street { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string? ZipCode { get; init; }
    public decimal PricePerNight { get; init; }
    public int MaxGuests { get; init; }
    public int Bedrooms { get; init; }
    public int Bathrooms { get; init; }
    public string Status { get; init; } = string.Empty;

    public string TypeName { get; init; } = string.Empty;
    public string OwnerName { get; init; } = string.Empty;

    // Collections
    public List<string> Amenities { get; init; } = new();
    public List<string> ImageUrls { get; init; } = new();
}