using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Domain.Entities
{
    public class Review : BaseEntity
    {
        public Guid ListingId { get; set; }
        public Guid UserId { get; set; } // الشخص الذي كتب التقييم
        public Guid? BookingId { get; set; } // اختياري: لربط التقييم بإقامة محددة

        public int Rating { get; set; } // من 1 إلى 5
        public string Comment { get; set; } = string.Empty;

        // Navigation properties
        public Listing Listing { get; set; } = null!;
        public User User { get; set; } = null!;
    }

    
}