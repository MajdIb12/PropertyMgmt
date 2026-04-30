using FluentValidation;

namespace PropertyMgmt.Application.Features.Tenants.Command.UpdateTenant;

public class UpdateTenantValidator : AbstractValidator<UpdateTenantCommand>
{
    public UpdateTenantValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Tenant ID is required.");
    }
}