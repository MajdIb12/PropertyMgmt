using FluentValidation;

namespace PropertyMgmt.Application.Features.Supscriptions.Query.GetAllSubscriptionByUserId
{
    public class GetAllSubscriptionByUserIdValidator : AbstractValidator<GetAllSubscriptionByUserIdQuery>
    {
        public GetAllSubscriptionByUserIdValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Plan Id is requerid");
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Page number must be greater than 0.");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page size must be greater than 0.");
        }
    }
}
