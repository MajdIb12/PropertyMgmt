using Microsoft.AspNetCore.Identity;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Domain.Common;

public abstract class ApplicationUser : IdentityUser<Guid>, IMayHaveTenant, ISoftDelete
{
    // حقول مشتركة بين الجميع
    public string FullName { get; set; } = string.Empty;
    public string? TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}