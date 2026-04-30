using System;
using MediatR;

namespace PropertyMgmt.Application.Features.Tenants.Command.DeleteTenant;

public record DeleteTenantCommand(string TenantId) : IRequest<bool>;
