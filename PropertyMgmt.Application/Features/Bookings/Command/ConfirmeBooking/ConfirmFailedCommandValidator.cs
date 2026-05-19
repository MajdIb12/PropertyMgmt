using FluentValidation;

namespace PropertyMgmt.Application.Features.Bookings.Command.ConfirmeBooking;

public class ConfirmFailedCommandValidator : AbstractValidator<ConfirmeBookingCommand>
{
    public ConfirmFailedCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Booking ID is required.");
    }
}