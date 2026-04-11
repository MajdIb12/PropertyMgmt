namespace PropertyMgmt.Domain.Entities;

public class MasterAdmin
{
    public int Id { get; set; }
    public string Name { get; set; } = "Super User";
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    // صلاحيات خاصة بالنظام ككل
    public bool CanCreateTenants { get; set; } = true; // إنشاء شركات جديدة
    public bool CanSuspendTenants { get; set; } = true; // إيقاف شركة عن العمل
    public bool CanViewAllAuditLogs { get; set; } = true; // رؤية سجلات الجميع
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}