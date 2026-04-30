using PropertyMgmt.Domain.Common;
using PropertyMgmt.Domain.Enums;

namespace PropertyMgmt.Domain.Entities;

public class Admin : ApplicationUser
{
    public AdminRole Role { get; set; } 
}
