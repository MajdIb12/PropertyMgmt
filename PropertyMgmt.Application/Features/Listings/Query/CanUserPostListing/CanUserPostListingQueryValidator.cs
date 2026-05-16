using FluentValidation;

namespace PropertyMgmt.Application.Features.Listings.Query.CanUserPostListing;

public class CanUserPostListingQueryValidator : AbstractValidator<CanUserPostListingQuery>
{
    public CanUserPostListingQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");
    }
}