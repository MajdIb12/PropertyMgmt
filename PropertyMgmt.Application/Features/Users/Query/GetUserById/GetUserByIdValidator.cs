using FluentValidation;

namespace PropertyMgmt.Application.Features.Users.Query;

public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("User Id is required.");
    }
}