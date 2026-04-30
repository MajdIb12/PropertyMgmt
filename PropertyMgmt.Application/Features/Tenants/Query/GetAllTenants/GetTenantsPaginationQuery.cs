using System;
using MediatR;
using PropertyMgmt.Application.Common.Model;

namespace PropertyMgmt.Application.Features.Tenants.Query.GetAllTenants;

public record GetTenantsPaginationQuery(int PageNumber, int PageSize): IRequest<PaginatedList<AllTenantDto>>;
