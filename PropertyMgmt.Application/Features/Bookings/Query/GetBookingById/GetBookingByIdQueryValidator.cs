using FluentValidation;

namespace PropertyMgmt.Application.Features.Bookings.Query.GetBookingById;

public class GetBookingByIdQueryValidator : AbstractValidator<GetBookingByIdQuery>
{
    public GetBookingByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Booking ID is required.");
    }
}