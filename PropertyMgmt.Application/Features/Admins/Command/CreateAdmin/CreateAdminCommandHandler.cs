using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Common;
using PropertyMgmt.Domain.Entities;
using PropertyMgmt.Domain.Enums;

namespace PropertyMgmt.Application.Features.Admins.Command.CreateAdmin;

public class CreateAdminCommandHandler(ITenantService tenantService, UserManager<ApplicationUser> userManager, IApplicationDbContext context) 
: IRequestHandler<CreateAdminCommand, Guid>
{

    public async Task<Guid> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var targetTenantId = tenantService.IsMasterAdmin 
            ? request.TenantId 
            : tenantService.TenantId;

        if (string.IsNullOrEmpty(targetTenantId))
            throw new NotFoundException(nameof(Tenant), targetTenantId ?? "null");

        var adminExists = await context.Admins
            .IgnoreQueryFilters() 
            .AnyAsync(u => u.TenantId == targetTenantId, cancellationToken);
        Admin admin;

        if (!adminExists)
        {
            if (!tenantService.IsMasterAdmin)
                throw new UnauthorizedAccessException("Only Master Admin can create the first Admin for a tenant.");
            var tenantemail = context.Tenants
                    .AsNoTracking()
                    .Where(t => t.Id == targetTenantId)
                    .Select(t => t.AdminEmail).FirstOrDefault();
            if (tenantemail == null ||tenantemail != request.Email)
                throw new ValidationExceptions(new Dictionary<string, string[]>
                {
                    { "Email", new[] { "First Admin email must match the Tenant's AdminEmail." } }
                });
             admin =await CreateAdminAsync(request, targetTenantId, AdminRole.SuperAdmin);
        }
        else
        {
            if (tenantService.IsMasterAdmin)
                throw new InvalidOperationException("First Admin already exists. Use a Tenant Admin account to add more.");

            // هنا نعتمد على الـ Role المرسل (SuperAdmin أو PropertyManager)
            admin = await CreateAdminAsync(request, targetTenantId, request.Role);
        }
        return admin.Id;
    }

    private async Task<Admin> CreateAdminAsync(CreateAdminCommand request, string tenantId, AdminRole role)
    {
        var user = new Admin
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            TenantId = tenantId,
            Role = role == AdminRole.SuperAdmin ? AdminRole.SuperAdmin : AdminRole.PropertyManager
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"User creation failed: {errors}");
        }

        await userManager.AddToRoleAsync(user, role.ToString());
        return user;
    }
}