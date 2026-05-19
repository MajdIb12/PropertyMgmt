using MediatR;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyMgmt.Application.Features.Bookings.Command.CreateBooking;
    public record CreateBookingCommand(
        Guid ListingId,
        Guid UserId,
        decimal TotalPrice,
        DateTime StartDate,
        string currencyCode,
        PaymentMethod PaymentMethod,
        DateTime EndDate) : IRequest<Guid>;
