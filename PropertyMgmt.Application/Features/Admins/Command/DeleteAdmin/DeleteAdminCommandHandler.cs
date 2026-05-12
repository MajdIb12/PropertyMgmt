using MediatR;
using Microsoft.AspNetCore.Identity;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Application.Features.Admins.Command.DeleteAdmin;

public class DeleteAdminCommandHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<DeleteAdminCommand, bool>
{

    public async Task<bool> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
    {
        var admin = await userManager.FindByIdAsync(request.Id.ToString())
            ?? throw new NotFoundException(nameof(Admins), request.Id);
        if (admin is ISoftDelete softDeleteAdmin)
        {
            softDeleteAdmin.IsDeleted = true;
            softDeleteAdmin.DeletedAt = DateTime.UtcNow;

            // نستخدم Update وليس Delete
            var result = await userManager.UpdateAsync(admin);
            return result.Succeeded;
        }
        throw new InvalidOperationException("The specified admin cannot be deleted.");
    }
}
