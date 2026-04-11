using PropertyMgmt.Domain.Common;
using PropertyMgmt.Domain.Enums;
using PropertyMgmt.Domain.ValueObjects;


namespace PropertyMgmt.Domain.Entities;


public class Listing : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Address Address { get; set; } = null!;
    public decimal PricePerNight { get; set; }
    public int MaxGuests { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }

    public ListingStatus Status { get; set; } = ListingStatus.Available;

    // داخل كلاس Property
    public Guid ListingTypeId { get; set; } // Foreign Key
    public ListingType ListingType { get; set; } = null!; // Navigation Property
    public Guid OwnerId { get; set; }
    
    public User Owner { get; set; } = null!;

    public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();

    public ICollection<ListingImage> Images { get; set; } = new List<ListingImage>();
}