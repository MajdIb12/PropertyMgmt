using Microsoft.AspNetCore.Http;
using PropertyMgmt.Application.Interfaces;
using System.Security.Claims;

namespace PropertyMgmt.Infrastructure.MultiTenancy;

public class TenantService : ITenantService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IApplicationDbContext _context; // نحتاج سياق قاعدة البيانات للبحث

    public TenantService(IHttpContextAccessor httpContextAccessor, IApplicationDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    

    public string? GetTenantId()
    {
        // محاولة جلب الـ ID من الـ Token أولاً (للمستخدمين المسجلين)
        var tenantIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue("TenantId");
        
        if (!string.IsNullOrEmpty(tenantIdClaim)) return tenantIdClaim;

        // خيار احتياطي: جلبها من الـ Items التي وضعها الـ Middleware (للزوار)
        return _httpContextAccessor.HttpContext?.Items["TenantId"]?.ToString();
    }

    public bool IsMasterAdmin()
    {
        return _httpContextAccessor.HttpContext?.User?.IsInRole("MasterAdmin") ?? false;
    }
}
