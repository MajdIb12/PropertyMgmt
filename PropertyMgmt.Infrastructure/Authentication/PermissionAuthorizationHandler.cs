using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PropertyMgmt.Infrastructure.Authentication;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        // 1. استخراج الـ Role المخزن في الـ JWT Token القياسي
        var userRole = context.User.FindFirst(ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(userRole))
        {
            return Task.CompletedTask; // لا يوجد دور، ارفض الطلب تلقائياً
        }

        // 2. جلب الصلاحيات الخاصة بهذا الدور من الـ In-Memory Mapping
        var permissions = RolePermissionMapping.GetPermissionsForRole(userRole);

        // 3. التحقق الفوري والسريع في الـ RAM
        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement); // الصلاحية مطابقة، اسمح بالمرور ✅
        }

        return Task.CompletedTask;
    }
}