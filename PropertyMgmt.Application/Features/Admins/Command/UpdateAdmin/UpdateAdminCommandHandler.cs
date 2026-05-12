using MediatR;
using Microsoft.AspNetCore.Identity;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Domain.Common;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Admins.Command.UpdateAdmin;

public class UpdateAdminCommandHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<UpdateAdminRoleCommand, bool>
{

    public async Task<bool> Handle(UpdateAdminRoleCommand request, CancellationToken cancellationToken)
{
    // 1. البحث عن المستخدم (سيعود كـ ApplicationUser)
    var user = await userManager.FindByIdAsync(request.Id.ToString())
        ?? throw new NotFoundException(nameof(Admin), request.Id);

    // 2. التحقق وعمل Casting إلى Admin
    if (user is not Admin admin)
    {
        throw new NotFoundException(nameof(Admin), request.Id);
    }

    // 3. تحديث الدور في خاصية الـ Enum الخاصة بك

    // 4. تحديث الـ Roles الخاصة بـ Identity (اختياري ولكن مهم)
    // أولاً نمسح الأدوار القديمة ثم نضيف الدور الجديد لضمان التزامن
    var currentRoles = await userManager.GetRolesAsync(admin);
    await userManager.RemoveFromRolesAsync(admin, currentRoles);
    await userManager.AddToRoleAsync(admin, request.Role.ToString());

    admin.Role = request.Role;
    await userManager.UpdateSecurityStampAsync(admin);
    // 5. حفظ التغييرات في قاعدة البيانات
    var result = await userManager.UpdateAsync(admin);
    
    return result.Succeeded;
}
}
