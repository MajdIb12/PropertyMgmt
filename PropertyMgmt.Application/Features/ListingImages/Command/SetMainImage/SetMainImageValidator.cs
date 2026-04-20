using FluentValidation;

namespace PropertyMgmt.Application.Features.ListingImages.Command.SetMainImage;

public class SetMainImageValidator : AbstractValidator<SetMainImageCommand>
{
    public SetMainImageValidator()
    {
        RuleFor(x => x.ListingId).NotEmpty();
        RuleFor(x => x.ImageId).NotEmpty();
    }
}