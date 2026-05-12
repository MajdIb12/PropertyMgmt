using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Api.Middleware;

public class SecurityStampValidatorMiddleware(RequestDelegate next)
{
    private static readonly TimeSpan ValidationInterval = TimeSpan.FromMinutes(5);

    public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager, IMemoryCache cache)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var stampInToken = context.User.FindFirstValue("SecurityStamp");
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                var cacheKey = $"SecurityStampValidator:{userId}";

                // 1. نحاول جلب الختم من الكاش (إذا وجدناه، فهو حتماً صالح ولم يتجاوز 5 دقائق)
                if (!cache.TryGetValue(cacheKey, out string? currentStamp))
                {
                    // 2. لم نجده في الكاش (إما أن الـ 5 دقائق انتهت، أو أنه أول طلب للمستخدم) -> نجلبه من القاعدة
                    var user = await userManager.FindByIdAsync(userId);
                    currentStamp = user?.SecurityStamp;

                    // 3. نحفظه في الكاش للطلبات القادمة (سيُحذف تلقائياً بعد 5 دقائق)
                    if (currentStamp != null)
                    {
                        cache.Set(cacheKey, currentStamp, ValidationInterval);
                    }
                }

                // 4. المقارنة النهائية (سواء جاء الختم من الكاش أو من القاعدة)
                if (currentStamp == null || currentStamp != stampInToken)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json"; 
                    await context.Response.WriteAsJsonAsync(new { message = "Token is no longer valid. Please login again." });
                    return;
                }
            }
        }

        await next(context); // كل شيء سليم، ليكمل الطلب طريقه
    }
}