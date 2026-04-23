
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Api.Midlleware;
public class TenantIdentificationMiddleware
{
    private readonly RequestDelegate _next;

    public TenantIdentificationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITenantStore tenantStore)
    {
        var host = context.Request.Host.Host; // مثلاً: majd.yoursite.com
        var segments = host.Split('.');

        // إذا كان الرابط يحتوي على Subdomain (أكثر من مقطعين)
        if (segments.Length > 2)
        {
            var subdomain = segments[0];

            // استرجاع المستأجر من قاعدة البيانات (أو الكاش لسرعة الأداء)
            var tenant = await tenantStore.GetTenantBySubdomain(subdomain);

            if (tenant != null)
            {
                context.Items["TenantId"] = tenant.Id.ToString();
            }
            else
            {
                // إذا لم نجد المستأجر، ننهي الطلب (عطالة منطقية)
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("This tenant does not exist.");
                return;
            }
        }

        await _next(context);
    }
}