using FluentValidation;

namespace PropertyMgmt.Application.Features.Tenants.Command.CreateTenant;

public class CreateTenantValidator : AbstractValidator<CreateTenantCommand>
{
    public CreateTenantValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tenant name is required.")
            .MaximumLength(100).WithMessage("Tenant name cannot exceed 100 characters.");

        RuleFor(x => x.Identifier)
            .NotEmpty().WithMessage("Tenant identifier is required.")
            .MaximumLength(50).WithMessage("Tenant identifier cannot exceed 50 characters.");

        RuleFor(x => x.SubscriptionEndDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Subscription end date must be in the future.")
            .When(x => x.SubscriptionEndDate.HasValue);

        RuleFor(x => x.AdminEmail)
            .EmailAddress().WithMessage("Admin email must be a valid email address.")
            .When(x => !string.IsNullOrEmpty(x.AdminEmail));

        RuleFor(x => x.CreatedByMasterAdminId)
            .NotEmpty().WithMessage("Created by Master Admin ID is required.");
    }
}