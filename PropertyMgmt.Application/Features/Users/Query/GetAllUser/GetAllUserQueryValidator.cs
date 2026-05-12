using FluentValidation;

namespace PropertyMgmt.Application.Features.Users.Query.GetAllUser;

public class GetAllUserQueryValidator : AbstractValidator<GetAllUserQuery>
{
    public GetAllUserQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Page number must be greater than 0.");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page size must be greater than 0.");
    }
}