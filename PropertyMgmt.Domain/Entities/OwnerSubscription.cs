using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Domain.Entities;

public class OwnerSubscription : BaseEntity
    {
        public Guid OwnerId { get; set; }
        public User Owner { get; set; } = null!;

        public Guid SubscriptionPlanId { get; set; }
        public SubscriptionPlan Subscription { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool IsActive {get; set; } = true;
    }
