using PropertyMgmt.Domain.Common;
using PropertyMgmt.Domain.Enums;

namespace PropertyMgmt.Domain.Entities;

public class Admin : BaseEntity
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public AdminRole Role { get; set; } 
}
