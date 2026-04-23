namespace PropertyMgmt.Application.Interfaces;

public interface ITenantService
{
    public string? GetTenantId();
       bool IsMasterAdmin();
}
