using FluentValidation;

namespace PropertyMgmt.Application.Features.Supscriptions.Command.CreateSubscription;

public class CreateSubscriptionValidator : AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required");
        RuleFor(x => x.SubscriptionPlanId).NotEmpty().WithMessage("SubscriptionPlan Id is required");
    }
}