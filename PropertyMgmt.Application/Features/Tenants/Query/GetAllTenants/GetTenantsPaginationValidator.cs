using FluentValidation;

namespace PropertyMgmt.Application.Features.Tenants.Query.GetAllTenants;

public class GetTenantsPaginationValidator: AbstractValidator<GetTenantsPaginationQuery>
{
    public GetTenantsPaginationValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Page number must be greater than 0.");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page size must be greater than 0.");
    }
}