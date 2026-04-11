using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Domain.Entities;
 public class Amenity : BaseEntity
 {
     public string Name { get; set; } = string.Empty;
     public string? Icon { get; set; }

     public ICollection<Listing> Listings { get; set; } = new List<Listing>();
 }
