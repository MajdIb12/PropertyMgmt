using MediatR;

namespace PropertyMgmt.Application.Features.Bookings.Command.DeleteBooking;

public record DeleteBookingCommand(Guid Id) : IRequest<bool>;
