using Microsoft.AspNetCore.Http;
using PropertyMgmt.Application.Interfaces;
using System.Security.Claims;

namespace PropertyMgmt.Infrastructure.MultiTenancy;

public class TenantService : ITenantService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetTenantId()
    {
        // 1. محاولة جلب الـ TenantId من الـ Claims (إذا كان المستخدم مسجل دخول)
        var tenantIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue("TenantId");

        if (!string.IsNullOrEmpty(tenantIdClaim))
        {
            return tenantIdClaim;
        }

        // 2. كخيار احتياطي: محاولة جلبها من الـ Header (مفيد عند تسجيل الدخول أو الـ Public API)
        // Header Name: "X-Tenant-Id"
        if (_httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantHeader) == true)
        {
            return tenantHeader;
        }

        return null;
    }

    public bool IsMasterAdmin()
    {
        // نتحقق مما إذا كان المستخدم يملك صلاحية MasterAdmin من الـ Token
        return _httpContextAccessor.HttpContext?.User?.IsInRole("MasterAdmin") ?? false;
    }
}