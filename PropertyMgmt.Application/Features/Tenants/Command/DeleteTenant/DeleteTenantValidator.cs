using FluentValidation;

namespace PropertyMgmt.Application.Features.Tenants.Command.DeleteTenant;

public class DeleteTenantValidator : AbstractValidator<DeleteTenantCommand>
{
    public DeleteTenantValidator()
    {
        RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant ID is required.");
    }
}