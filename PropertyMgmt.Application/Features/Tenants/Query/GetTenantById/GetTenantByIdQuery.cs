using System;
using MediatR;

namespace PropertyMgmt.Application.Features.Tenants.Query.GetTenantById;

public record GetTenantByIdQuery(string Id) : IRequest<TenantDto>;
