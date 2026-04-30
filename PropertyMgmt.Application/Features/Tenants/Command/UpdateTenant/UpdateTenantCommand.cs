using System;
using MediatR;

namespace PropertyMgmt.Application.Features.Tenants.Command.UpdateTenant;

public record UpdateTenantCommand(
    string Id,
    string Name,
    string Identifier,
    bool IsActive,
    DateTime? SubscriptionEndDate,
    string? AdminEmail
) : IRequest<Unit>;
