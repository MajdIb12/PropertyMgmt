using PropertyMgmt.Application.Common.Model.IdentityDtos;

namespace PropertyMgmt.Application.Interfaces;

public interface IIdentityService
{
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    Task<bool> RegisterUserAsync(RegisterRequestDto request);
    Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
}