using System;
using MediatR;
using PropertyMgmt.Domain.Entities;
using PropertyMgmt.Domain.Enums;

namespace PropertyMgmt.Application.Features.Admins.Command.CreateAdmin;

public record CreateAdminCommand(
    string FullName,
    string Email,
    string Password,
    string? TenantId,
    AdminRole Role
) : IRequest<Guid>;
