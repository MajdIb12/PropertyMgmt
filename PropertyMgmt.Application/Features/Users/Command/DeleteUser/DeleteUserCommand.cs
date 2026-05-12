using System;
using MediatR;

namespace PropertyMgmt.Application.Features.Users.Command.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest<bool>;
