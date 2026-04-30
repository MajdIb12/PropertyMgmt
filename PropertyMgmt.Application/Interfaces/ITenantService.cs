namespace PropertyMgmt.Application.Interfaces;

public interface ITenantService
{
    public string? TenantId { get; }
    public bool IsMasterAdmin { get; }
}
