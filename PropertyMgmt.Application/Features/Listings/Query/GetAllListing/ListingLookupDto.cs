using System.Linq.Expressions;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Listings.Query.GetAllListings;

public record ListingLookupDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string AddressSummary { get; init; } = string.Empty;
    public decimal PricePerNight { get; init; }
    public string TypeName { get; init; } = string.Empty;
    public string MainImageUrl { get; init; } = string.Empty;

    // هنا السحر!
    public static Expression<Func<Listing, ListingLookupDto>> Projection
    {
        get
        {
            return l => new ListingLookupDto
            {
                Id = l.Id,
                Name = l.Name,
                PricePerNight = l.PricePerNight,
                TypeName = l.ListingType.Name,
                AddressSummary = l.Address.City + ", " + l.Address.Street,
                MainImageUrl = l.Images
                    .Where(i => i.IsPrimary)
                    .Select(i => i.ImageUrl)
                    .FirstOrDefault() ?? "default-image.jpg"
            };
        }
    }
}
