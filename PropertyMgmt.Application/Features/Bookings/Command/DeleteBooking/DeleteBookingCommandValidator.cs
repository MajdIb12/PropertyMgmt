using FluentValidation;

namespace PropertyMgmt.Application.Features.Bookings.Command.DeleteBooking;

public class DeleteBookingCommandValidator : AbstractValidator<DeleteBookingCommand>
{
    public DeleteBookingCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Booking Id is required.");
    }
}
