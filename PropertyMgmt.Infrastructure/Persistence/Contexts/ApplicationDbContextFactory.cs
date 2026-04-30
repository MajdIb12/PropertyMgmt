using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Infrastructure.Contexts;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlServer("Server=.;Database=PropertyMgmtDb;User Id=sa;Password=sa123456;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True");

        return new ApplicationDbContext(optionsBuilder.Options, new DesignTimeTenantService());
    }
}

// كلاس وهمي فقط لإرضاء الـ Constructor وقت الـ Migration
public class DesignTimeTenantService : ITenantService
{
    public string? TenantId => null;
    public bool IsMasterAdmin => true;
}