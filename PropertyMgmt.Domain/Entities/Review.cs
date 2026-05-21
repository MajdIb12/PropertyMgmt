using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Domain.Entities
{
    public class Review : BaseEntity
    {
        public Guid ListingId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid? BookingId { get; private set; }

        public int Rating { get; private set; }
        public string Comment { get; private set; } = string.Empty;

        public Listing Listing { get; set; } = null!;
        public User User { get; set; } = null!;

        private Review() { }

        public Review(Guid listingId, Guid userId, Guid? bookingId, int rating, string comment, string tenantId)
        {
            if (rating < 1 || rating > 5)
                throw new DomainException("Rating must be between 1 and 5.");

            if (string.IsNullOrWhiteSpace(comment))
                throw new DomainException("Comment cannot be empty.");

            ListingId = listingId;
            UserId = userId;
            BookingId = bookingId;
            Rating = rating;
            Comment = comment;
            TenantId = tenantId;
        }
    }


}