using MediatR;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Tenants.Command.CreateTenant;

public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, string>
{
    private readonly IApplicationDbContext _context;

    public CreateTenantCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = new Tenant
        {
            Name = request.Name,
            Identifier = request.Identifier,
            SubscriptionEndDate = request.SubscriptionEndDate,
            AdminEmail = request.AdminEmail,
            CreatedByMasterAdminId = request.CreatedByMasterAdminId.ToString()
        };

        await _context.Tenants.AddAsync(tenant);
        await _context.SaveChangesAsync(cancellationToken);
        return tenant.Id;
    }
}