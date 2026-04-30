using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddOpenApi();

        
        services.AddCors(options => {
            options.AddPolicy("DefaultPolicy", policy => {
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        services.AddAuthentication(options =>
            {
                // نخبر .NET أن الطريقة الافتراضية للتحقق هي عبر JWT
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // اجعلها true في بيئة الـ Production
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // أهم شرط: التحقق من التوقيع
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.Key)),
                    
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    
                    ValidateLifetime = true, // التحقق من تاريخ انتهاء التوكن
                    ClockSkew = TimeSpan.Zero // إلغاء الدقائق الخمس الإضافية التي يضيفها النظام كفترة سماح
                };
            });

        return services;
    }
}