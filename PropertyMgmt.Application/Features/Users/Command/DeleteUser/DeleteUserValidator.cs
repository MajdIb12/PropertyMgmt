using FluentValidation;

namespace PropertyMgmt.Application.Features.Users.Command.DeleteUser;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("User Id is required.");
    }
}