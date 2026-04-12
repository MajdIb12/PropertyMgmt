using FluentValidation;

namespace PropertyMgmt.Application.Features.Listings.Commands.DeleteListing;

public class DeleteListingValidator : AbstractValidator<DeleteListingCommand>
{
    public DeleteListingValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Listing ID is required.");
    }
}