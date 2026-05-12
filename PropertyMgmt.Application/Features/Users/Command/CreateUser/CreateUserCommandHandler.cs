using MediatR;
using PropertyMgmt.Application.Common.Model.IdentityDtos;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Users.Command.CreateUser;

public class CreateUserCommandHandler(IIdentityService identityService) : IRequestHandler<CreateUserCommand, bool>
{
    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await identityService.RegisterUserAsync(new RegisterRequestDto
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName
        });
    }
}