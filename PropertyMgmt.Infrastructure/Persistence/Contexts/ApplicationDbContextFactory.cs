using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Infrastructure.Contexts;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var Conn = args.Length > 0 ? args[0] : "Server=.;Database=PropertyMgmtDb;User Id=sa;Password=sa123456;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True";

        optionsBuilder.UseSqlServer(Conn);

        return new ApplicationDbContext(optionsBuilder.Options, new DesignTimeTenantService(), new DesginCurrentUserService());
    }
}

public class DesignTimeTenantService : ITenantService
{
    public string? TenantId => null;
    public bool IsMasterAdmin => true;
}
public class DesginCurrentUserService : ICurrentUserService
{
    public string? UserId => string.Empty;
}