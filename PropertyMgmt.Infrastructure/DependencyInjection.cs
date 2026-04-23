using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Infrastructure.Contexts;
using PropertyMgmt.Infrastructure.MultiTenancy;
using PropertyMgmt.Infrastructure.Services;

namespace PropertyMgmt.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        
        services.AddHttpContextAccessor();

        // 3. تسجيل خدمات الـ Multi-tenancy
        services.AddScoped<ITenantService, TenantService>();

        services.AddScoped<ITenantStore, TenantStore>();

        
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IFileService, LocalFileService>();

        return services;
    }
}