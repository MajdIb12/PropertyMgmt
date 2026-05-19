using FluentValidation;

namespace PropertyMgmt.Application.Features.Bookings.Command.CreateBooking;

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(x => x.ListingId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.TotalPrice).GreaterThan(0);
        RuleFor(x => x.PaymentMethod).NotEmpty();
        RuleFor(x => x.currencyCode).NotEmpty();
        RuleFor(x => x.StartDate).GreaterThan(DateTime.UtcNow).WithMessage("Start date must be in the future.");
        RuleFor(x => x.StartDate).LessThan(x => x.EndDate).WithMessage("Start date must be before end date.");
    }
}
