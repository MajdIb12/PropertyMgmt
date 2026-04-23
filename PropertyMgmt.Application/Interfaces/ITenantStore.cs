using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Interfaces;

public interface ITenantStore
{
    public Task<Tenant?> GetTenantBySubdomain(string subdomain);
}