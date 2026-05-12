using FluentValidation;

namespace PropertyMgmt.Application.Features.Admins.Query.GetAdminById;

public class GetAdminByIdValidator : AbstractValidator<GetAdminByIdQuery>
{
    public GetAdminByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Admin ID is required.");
    }
}
