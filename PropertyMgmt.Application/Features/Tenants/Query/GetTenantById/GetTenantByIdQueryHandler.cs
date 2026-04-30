using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Tenants.Query.GetTenantById;

public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, TenantDto>
{
    private readonly IApplicationDbContext _Context;

    public GetTenantByIdQueryHandler(IApplicationDbContext context)
    {
        _Context = context;
    }

    public async Task<TenantDto> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
    {
        var tenant = await _Context.Tenants.FirstOrDefaultAsync(t => t.Id == request.Id)
                    ?? throw new NotFoundException(nameof(Tenants), request.Id);

        return new TenantDto        
        {
            Id = tenant.Id,
            Name = tenant.Name,
            Identifier = tenant.Identifier,
            IsActive = tenant.IsActive,
            SubscriptionEndDate = tenant.SubscriptionEndDate,
            AdminEmail = tenant.AdminEmail,
            CreatedAt = tenant.CreatedAt,
            CreatedByMasterAdminId = tenant.CreatedByMasterAdminId
        };
    }
}