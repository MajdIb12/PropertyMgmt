namespace PropertyMgmt.Application.Interfaces;

public interface ITenantService
{
    // جلب معرف الشركة الحالي
    string? GetTenantId();
    
    // هل المستخدم الحالي هو Master Admin؟ (مفيد لتجاوز الفلاتر)
    bool IsMasterAdmin();
}