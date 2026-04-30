using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Domain.Entities;

public class Tenant
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = string.Empty;

    public string Identifier { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime? SubscriptionEndDate { get; set; }

    public string? AdminEmail { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string CreatedByMasterAdminId { get; set; } = string.Empty;
    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}
