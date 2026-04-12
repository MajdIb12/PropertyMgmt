using FluentValidation;

namespace PropertyMgmt.Application.Features.ListingTypes.Command.CreateListingType;

public class CreateListingTypeValidator : AbstractValidator<CreateListingTypeCommand>
{
    public CreateListingTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
} 