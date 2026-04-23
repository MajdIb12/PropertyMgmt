using FluentValidation;

namespace PropertyMgmt.Application.Features.Listings.Commands.SetMainImage;

public class SetMainImageValidator : AbstractValidator<SetMainImageCommand>
{
    public SetMainImageValidator()
    {
        RuleFor(x => x.ListingId).NotEmpty();
        RuleFor(x => x.ImageId).NotEmpty();
    }
}