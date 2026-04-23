using FluentValidation;

namespace PropertyMgmt.Application.Features.Listings.Commands.DeleteImage;

public class DeleteListingImageValidator : AbstractValidator<DeleteListingImageCommand>
{
    public DeleteListingImageValidator()
    {
        RuleFor(x => x.ListingId).NotEmpty().WithMessage("ListingId is required.");
        RuleFor(x => x.ImageId).NotEmpty().WithMessage("ImageId is required.");
    }
}