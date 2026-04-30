namespace PropertyMgmt.Application.Common.Model.IdentityDtos;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;

    public string? TenantId {get; set;}
}
