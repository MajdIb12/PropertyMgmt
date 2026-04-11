using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Infrastructure.Contexts;
using PropertyMgmt.Infrastructure.MultiTenancy;

namespace PropertyMgmt.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. تسجيل الـ DbContext وقراءة الـ Connection String
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // 2. تسجيل الـ HttpContextAccessor (ضروري للـ TenantService)
        services.AddHttpContextAccessor();

        // 3. تسجيل خدمات الـ Multi-tenancy
        services.AddScoped<ITenantService, TenantService>();

        // 4. تسجيل الـ Interface الخاص بالـ Context (لكي تراه طبقة الـ Application)
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}