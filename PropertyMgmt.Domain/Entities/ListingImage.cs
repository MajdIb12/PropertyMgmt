using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Domain.Entities;
    public class ListingImage : BaseEntity
    {
        public string ImageUrl { get; set; } = string.Empty;
        public int publicId { get; set; }
        public bool IsPrimary { get; set; } = false;
        
        public Guid ListingId { get; set; } // Foreign Key
        public Listing Listing { get; set; } = null!; // Navigation Property
    }