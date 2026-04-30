namespace PropertyMgmt.Application.Features.Tenants.Query.GetTenantById;

public class TenantDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Identifier { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime? SubscriptionEndDate { get; set; }
    public string? AdminEmail { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedByMasterAdminId { get; set; } = string.Empty;
}
