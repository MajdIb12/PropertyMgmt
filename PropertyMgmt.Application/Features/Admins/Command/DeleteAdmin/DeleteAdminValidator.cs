using FluentValidation;

namespace PropertyMgmt.Application.Features.Admins.Command.DeleteAdmin;

public class DeleteAdminValidator : AbstractValidator<DeleteAdminCommand>
{
    public DeleteAdminValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Admin Id is required.");
    }
}