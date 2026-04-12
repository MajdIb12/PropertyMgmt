using FluentValidation;
namespace PropertyMgmt.Application.Features.Listings.Commands.CreateListing;

public class UpdateListingValidator : AbstractValidator<UpdateListingCommand>
{
    public UpdateListingValidator()
    {
       RuleFor(v => v.Id)
            .NotEmpty().WithMessage("معرف العقار مطلوب");
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("العنوان مطلوب")
            .MaximumLength(200).WithMessage("العنوان يجب ألا يتجاوز 200 حرف");

        RuleFor(v => v.PricePerNight)
            .GreaterThan(0).WithMessage("السعر يجب أن يكون أكبر من صفر");

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("الوصف مطلوب");

        RuleFor(v => v.MaxGuests)
            .GreaterThan(0).WithMessage("عدد الضيوف يجب أن يكون أكبر من صفر");
    }
}