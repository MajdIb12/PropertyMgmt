using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities; // تأكد من المسار الصحيح للـ Tenant

namespace PropertyMgmt.Infrastructure.MultiTenancy;

public class TenantStore : ITenantStore
{
    private readonly IApplicationDbContext _context;

    public TenantStore(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Tenant?> GetTenantBySubdomain(string subdomain)
    {
        return await _context.Tenants.AsNoTracking().FirstOrDefaultAsync(t => t.Identifier == subdomain);
    }
}