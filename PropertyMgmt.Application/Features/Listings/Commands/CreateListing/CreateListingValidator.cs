using FluentValidation;
namespace PropertyMgmt.Application.Features.Listings.Commands.CreateListing;

public class CreateListingValidator : AbstractValidator<CreateListingCommand>
{
    public CreateListingValidator()
    {
      
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("العنوان مطلوب")
            .MaximumLength(200).WithMessage("العنوان يجب ألا يتجاوز 200 حرف");

        RuleFor(v => v.PricePerNight)
            .GreaterThan(0).WithMessage("السعر يجب أن يكون أكبر من صفر");

        RuleFor(v => v.Bathrooms)
            .GreaterThanOrEqualTo(0).WithMessage("عدد الحمامات لا يمكن أن يكون سالبًا");

        RuleFor(v => v.Bedrooms)
            .GreaterThanOrEqualTo(0).WithMessage("عدد غرف النوم لا يمكن أن يكون سالبًا");

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("الوصف مطلوب");

        RuleFor(v => v.City)
            .NotEmpty().WithMessage("المدينة مطلوبة");

        RuleFor(v => v.Street)
            .NotEmpty().WithMessage("الشارع مطلوب");
            
        RuleFor(v => v.ListingTypeId)
            .NotEqual(Guid.Empty).WithMessage("يجب اختيار نوع العقار");
    }
}