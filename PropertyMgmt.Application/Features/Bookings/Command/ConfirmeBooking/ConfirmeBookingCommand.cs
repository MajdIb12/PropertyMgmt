using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyMgmt.Application.Features.Bookings.Command.ConfirmeBooking;
public record ConfirmeBookingCommand(Guid Id) : IRequest<bool>;
