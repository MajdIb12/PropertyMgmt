using FluentValidation;

namespace PropertyMgmt.Application.Features.Tenants.Query.GetTenantById;

public class GetTenantByIdValidator : AbstractValidator<GetTenantByIdQuery>
{
    public GetTenantByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Tenant ID is required.");
    }
}