using FluentValidation;

namespace PropertyMgmt.Application.Features.Users.Query.CanUserPostListings;

public class CanUserPostListingsValidator : AbstractValidator<CanUserPostListingsQuery>
{
    public CanUserPostListingsValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required");
    }
}