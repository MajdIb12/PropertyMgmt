using FluentValidation;

namespace PropertyMgmt.Application.Features.Supscriptions.Query.GetAllSubscriptionByPlanId
{
    public class GetAllSubscriptionByPlanIdValidator : AbstractValidator<GetAllSubscriptionByPlanIdQuery>
    {
        public GetAllSubscriptionByPlanIdValidator()
        {
            RuleFor(x => x.PlanId).NotEmpty().WithMessage("Plan Id is requerid");
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Page number must be greater than 0.");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page size must be greater than 0.");
        }
    }
}
