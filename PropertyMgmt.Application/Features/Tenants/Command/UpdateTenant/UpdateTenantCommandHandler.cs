using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Tenants.Command.UpdateTenant;

public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateTenantCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = await _context.Tenants.FirstOrDefaultAsync(t => t.Id == request.Id);
        if (tenant == null)
        {
            throw new NotFoundException(nameof(Tenant), request.Id);
        }

        tenant.Name = request.Name;
        tenant.Identifier = request.Identifier;
        tenant.IsActive = request.IsActive;
        tenant.SubscriptionEndDate = request.SubscriptionEndDate;
        tenant.AdminEmail = request.AdminEmail;

         _context.Tenants.Update(tenant);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
