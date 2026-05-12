using PropertyMgmt.Domain.Entities;

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

    public AdminForTenantDto Admin { get; set; } = new AdminForTenantDto();
}

    public class AdminForTenantDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}