using System;
using MediatR;
using PropertyMgmt.Domain.Enums;

namespace PropertyMgmt.Application.Features.Admins.Command.UpdateAdmin;

public record UpdateAdminRoleCommand(
    Guid Id,
    AdminRole Role
) : IRequest<bool>;
