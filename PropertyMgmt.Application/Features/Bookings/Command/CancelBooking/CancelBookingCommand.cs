using MediatR;

namespace PropertyMgmt.Application.Features.Bookings.Command.CancelBooking;
  public record CancelBookingCommand(Guid BookingId) : IRequest<bool>;
