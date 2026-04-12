using FluentValidation;

namespace PropertyMgmt.Application.Features.ListingTypes.Command.UpdateListingType;
public class UpdateListingTypeValidator : AbstractValidator<UpdateListingTypeCommand>
{
    public UpdateListingTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");
    }
}