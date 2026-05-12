using MediatR;

namespace PropertyMgmt.Application.Features.Users.Command.CreateUser;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<bool>;
