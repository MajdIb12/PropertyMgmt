using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Domain.Entities;

public class ListingType : BaseEntity
{
    public string Name { get; set; } = string.Empty; // مثل: Villa, Apartment
    public string Description { get; set; } = string.Empty;

    // Navigation Property: كل نوع يندرج تحته قائمة عقارات
    public ICollection<Listing> Listings { get; set; } = new List<Listing>();
}