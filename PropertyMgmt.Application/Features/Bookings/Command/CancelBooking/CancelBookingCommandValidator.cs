using FluentValidation;

namespace PropertyMgmt.Application.Features.Bookings.Command.CancelBooking;

public class CancelBookingCommandValidator : AbstractValidator<CancelBookingCommand>
{
    public CancelBookingCommandValidator()
    {
        RuleFor(x => x.BookingId).NotEmpty().WithMessage("Booking ID is required.");
    }
}