using FluentValidation;

namespace PropertyMgmt.Application.Features.Supscriptions.Command.UpdateSubscribtionPlan;

public class UpdateSubscriptionCommandValidator : AbstractValidator<UpdateSubscriptionCommand>
{
    public UpdateSubscriptionCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.MaxListings).GreaterThan(0).WithMessage("MaxListings must be greater than 0.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        RuleFor(x => x.DurationInMonths).GreaterThan(0).WithMessage("DurationInMonths must be greater than 0.");
    }
}