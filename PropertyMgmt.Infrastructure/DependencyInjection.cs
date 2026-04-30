using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Common;
using PropertyMgmt.Infrastructure.Contexts;
using PropertyMgmt.Infrastructure.MultiTenancy;
using PropertyMgmt.Infrastructure.Services;

namespace PropertyMgmt.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
{
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    services.AddHttpContextAccessor();

        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString,
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

    services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());


    services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(option =>
    {
        option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        option.Lockout.MaxFailedAccessAttempts = 5;
        option.Lockout.AllowedForNewUsers = true;
    })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    services.AddMemoryCache();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IIdentityService, IdentityService>();
    services.AddScoped<ITenantService, TenantService>();
    services.AddScoped<ITenantStore, TenantStore>();


    services.AddScoped<IFileService, LocalFileService>();

    return services;
}
}