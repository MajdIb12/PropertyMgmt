using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Common.Model.IdentityDtos;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Common;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;

    public IdentityService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
{
        var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new NotFoundException(nameof(User), userId);

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    
    return result.Succeeded;
}

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
       var user = await _userManager.Users
        .IgnoreQueryFilters() 
        .FirstOrDefaultAsync(u => u.Email == request.Email)
        ?? throw new UnauthorizedAccessException("Invalid credentials");

        if (await _userManager.IsLockedOutAsync(user))
            throw new UnauthorizedAccessException("Account is locked. Try again later.");

        var isValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isValid)
        {
            await _userManager.AccessFailedAsync(user);
            throw new UnauthorizedAccessException("Invalid credentials");
        } 
        await _userManager.ResetAccessFailedCountAsync(user);

     
        var token = _tokenService.CreateToken(user);

        return new AuthResponseDto 
        { 
            Token = token, 
            Email = user.Email!,
            FullName = user.FullName,
            TenantId = user.TenantId
        };
    }

    public async Task<bool> RegisterUserAsync(RegisterRequestDto request)
    {
        var newUser = new User 
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            TenantId = request.TenantId, 
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);
        return result.Succeeded;
    }

    
}