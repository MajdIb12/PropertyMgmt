using System;
using MediatR;

namespace PropertyMgmt.Application.Features.Tenants.Command.CreateTenant;

public record CreateTenantCommand(
    string Name,
    string Identifier,
    DateTime? SubscriptionEndDate,
    string? AdminEmail,
    Guid CreatedByMasterAdminId
): IRequest<string>;
