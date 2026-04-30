using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Domain.Entities;

public class MasterAdmin : ApplicationUser
{

    // صلاحيات خاصة بالنظام ككل
    public bool CanCreateTenants { get; set; } = true; // إنشاء شركات جديدة
    public bool CanSuspendTenants { get; set; } = true; // إيقاف شركة عن العمل
    public bool CanViewAllAuditLogs { get; set; } = true; // رؤية سجلات الجميع
}