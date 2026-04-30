namespace PropertyMgmt.Domain.Common;

public interface IMayHaveTenant
{
    public string? TenantId { get; set; }
}