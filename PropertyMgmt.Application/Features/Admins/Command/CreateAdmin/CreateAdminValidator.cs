using FluentValidation;

namespace PropertyMgmt.Application.Features.Admins.Command.CreateAdmin;

public class CreateAdminValidator : AbstractValidator<CreateAdminCommand>
{
    public CreateAdminValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
        RuleFor(x => x.Role).IsInEnum().WithMessage("Role must be a valid AdminRole(SuperAdmin or PropertyManager).");
        RuleFor(x => x.TenantId).NotEmpty().When(x => x.Role == Domain.Enums.AdminRole.PropertyManager)
            .WithMessage("TenantId is required for Property Managers.");
    }
}