using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Application.Interfaces;

public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
