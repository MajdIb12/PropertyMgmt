using System;
using MediatR;

namespace PropertyMgmt.Application.Features.Admins.Command.DeleteAdmin;

public record DeleteAdminCommand(Guid Id) : IRequest<bool>;
