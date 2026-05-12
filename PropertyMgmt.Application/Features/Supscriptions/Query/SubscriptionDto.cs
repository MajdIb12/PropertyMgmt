namespace PropertyMgmt.Application.Features.Supscriptions.Query
{
    public class SubscriptionDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public Guid SubsciptionPlanId { get; set; }
        public string SubsciptionPlanName { get; set;} = string.Empty;
    }
}