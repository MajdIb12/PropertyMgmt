using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Tenants.Query.GetAllTenants;

public class GetTenantsPaginationQueryHandler : IRequestHandler<GetTenantsPaginationQuery, PaginatedList<AllTenantDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTenantsPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<AllTenantDto>> Handle(GetTenantsPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Tenants.AsNoTracking()
            .Select(t => new AllTenantDto
            {
                Id = t.Id,
                Name = t.Name,
                Identifier = t.Identifier
            });
        
        return await PaginatedList<AllTenantDto>.CreateAsync(query, request.PageNumber, request.PageSize, cancellationToken);
    }
}
