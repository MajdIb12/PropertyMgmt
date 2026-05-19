using FluentValidation;

namespace PropertyMgmt.Application.Features.Bookings.Query.GetAllBookingByUserId;

public class GetAllBookingByUserIdQueryValidator : AbstractValidator<GetAllBookingByUserIdQuery>
{
    public GetAllBookingByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("PageNumber must be greater than 0.");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize must be greater than 0.");
    }
}