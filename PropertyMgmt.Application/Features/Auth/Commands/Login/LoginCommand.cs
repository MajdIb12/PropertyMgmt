using MediatR;
using PropertyMgmt.Application.Common.Model.IdentityDtos;

namespace PropertyMgmt.Application.Features.Auth.Commands.Login;
public record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto>;
