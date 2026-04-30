using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Interfaces;

public interface ITenantStore
{
    public Task<string?> GetTenantBySubdomain(string subdomain);
}