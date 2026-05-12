using FluentValidation;

namespace PropertyMgmt.Application.Features.Admins.Command.UpdateAdmin;

public class UpdateAdminValidator : AbstractValidator<UpdateAdminRoleCommand>
{
    public UpdateAdminValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Admin Id is required.");
        RuleFor(x => x.Role).IsInEnum().WithMessage("Invalid admin role.");
    }
}