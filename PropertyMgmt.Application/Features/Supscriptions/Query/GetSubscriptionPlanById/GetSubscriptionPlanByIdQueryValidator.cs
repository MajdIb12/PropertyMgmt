using FluentValidation;

namespace PropertyMgmt.Application.Features.Supscriptions.Query.GetSubscriptionPlanById;

public class GetSubscriptionPlanByIdQueryValidator : AbstractValidator<GetSubscriptionPlanByIdQuery>
{
    public GetSubscriptionPlanByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Subscription plan ID is required.");
    }
}