using FluentValidation;
namespace PropertyMgmt.Application.Features.Listings.Commands.CreateListing;

public class CreateListingValidator : AbstractValidator<CreateListingCommand>
{
    public CreateListingValidator()
    {
        // قواعد التحقق - لاحظ السهولة في القراءة
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("العنوان مطلوب")
            .MaximumLength(200).WithMessage("العنوان يجب ألا يتجاوز 200 حرف");

        RuleFor(v => v.PricePerNight)
            .GreaterThan(0).WithMessage("السعر يجب أن يكون أكبر من صفر");

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("الوصف مطلوب");

        RuleFor(v => v.City)
            .NotEmpty().WithMessage("المدينة مطلوبة");
            
        // يمكنك إضافة قواعد معقدة هنا
        RuleFor(v => v.ListingTypeId)
            .NotEqual(Guid.Empty).WithMessage("يجب اختيار نوع العقار");
    }
}