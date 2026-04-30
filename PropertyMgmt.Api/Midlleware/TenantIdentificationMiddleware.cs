
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Infrastructure.MultiTenancy;

namespace PropertyMgmt.Api.Middleware;
public class TenantIdentificationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ITenantStore domainResolver)
    {
        var host = context.Request.Host.Host.ToLower(); // مثلاً: tartous.majd.com

        if (host == "localhost")
        {
            await next(context);
            return;
        }

        if (host.EndsWith(".localhost"))
        {
            var subdomain = host.Replace(".localhost", ""); 

            var tenantId = await domainResolver.GetTenantBySubdomain(subdomain);

            if (!string.IsNullOrEmpty(tenantId))
            {
                context.Items["TenantId"] = tenantId;
                await next(context);
                return;
            }
        }

        
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsync("This company is not registered on our platform.");
    }
}