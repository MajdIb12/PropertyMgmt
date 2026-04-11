using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Domain.Entities;
    public class SubscriptionPlan : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int MaxListings { get; set; }
        public decimal Price { get; set; }
        public int DurationInMonths { get; set; }
    }
