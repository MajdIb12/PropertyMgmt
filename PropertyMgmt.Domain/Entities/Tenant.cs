namespace PropertyMgmt.Domain.Entities;

public class Tenant
{
    // المعرف الفريد (ID) - يفضل استخدامه في العلاقات البرمجية
    public string Id { get; set; } = Guid.NewGuid().ToString();

    // اسم الشركة (مثلاً: "شركة الرواد للعقارات")
    public string Name { get; set; } = string.Empty;

    // اسم فريد في الرابط (مثلاً: al-rowad) 
    // مفيد جداً إذا أردت عمل Subdomain مستقبلاً مثل al-rowad.propertymgmt.com
    public string Identifier { get; set; } = string.Empty;

    // حالة الاشتراك (Active, Suspended, Expired)
    public bool IsActive { get; set; } = true;

    // تاريخ انتهاء الاشتراك (أساسي في أنظمة الـ SaaS)
    public DateTime? SubscriptionEndDate { get; set; }

    // معلومات التواصل الأساسية للشركة
    public string? AdminEmail { get; set; }
    
    // تاريخ الإنشاء
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int CreatedByMasterAdminId { get; set; }
    public MasterAdmin? CreatedByMasterAdmin { get; set; }
}
