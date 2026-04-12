using FluentValidation;

namespace PropertyMgmt.Application.Features.ListingTypes.Command.DeleteListingType;
public class DeleteListingTypeValidator : AbstractValidator<DeleteListingTypeCommand>
{
    public DeleteListingTypeValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("معرف نوع العقار مطلوب");
    }
}