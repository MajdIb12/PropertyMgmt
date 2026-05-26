using Microsoft.AspNetCore.Authorization;

namespace PropertyMgmt.Infrastructure.Authentication;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }
    public PermissionRequirement(string permission) => Permission = permission;
}