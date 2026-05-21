using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyMgmt.Application.Features.Notifications.BookingRequest;
    public record BookingRequestedEvent(
    Guid BookingId,
    Guid OwnerId,
    string GuestName,
    string PropertyTitle,
    string TenantId
) : INotification;
