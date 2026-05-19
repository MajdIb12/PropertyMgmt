using FluentValidation;

namespace PropertyMgmt.Application.Features.Bookings.Query.GetAllbookingByOwnerId;

public class GetAllBookingByOwnerIdQueryValidator : AbstractValidator<GetAllBookingByOwnerIdQuery>
{
    public GetAllBookingByOwnerIdQueryValidator()
    {
        RuleFor(x => x.OwnerId).NotEmpty().WithMessage("OwnerId is required.");
        RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("PageNumber must be greater than 0.");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize must be greater than 0.");
    }
}
