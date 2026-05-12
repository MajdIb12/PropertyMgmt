using MediatR;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Users.Command.DeleteUser;

public class DeleteUserCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Customers.FindAsync(request.Id)
        ?? throw new NotFoundException(nameof(Users), request.Id);

        context.Customers.Remove(user);
        return true; // User deleted successfully
    }
}
