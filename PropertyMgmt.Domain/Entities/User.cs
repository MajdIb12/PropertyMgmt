using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Domain.Entities;
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public ICollection<Listing> OwnedListings { get; set; } = new List<Listing>();

        public ICollection<OwnerSubscription> Subscriptions { get; set; } = new List<OwnerSubscription>();
        public ICollection<Booking> MyBookings { get; set; } = new List<Booking>();
    }
