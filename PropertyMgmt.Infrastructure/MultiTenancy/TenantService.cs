using Microsoft.AspNetCore.Http;
using PropertyMgmt.Application.Interfaces;
using System.Security.Claims;

namespace PropertyMgmt.Infrastructure.MultiTenancy;

public class TenantService : ITenantService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private string? _cachedTenantId;
    private bool? _cachedIsMaster;

    public TenantService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? TenantId => _cachedTenantId ??= GetTenantFromContext();
    public bool IsMasterAdmin => _cachedIsMaster ??= CheckMasterRole();

    private string? GetTenantFromContext()
    {
        var context = _httpContextAccessor.HttpContext;
        var claimValue = context?.User?.FindFirstValue("TenantId");
        
        return !string.IsNullOrEmpty(claimValue) 
               ? claimValue 
               : context?.Items["TenantId"]?.ToString();
    }

    private bool CheckMasterRole()
    {
        return _httpContextAccessor.HttpContext?.User?.IsInRole("MasterAdmin") ?? false;
    }
}
