using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Features.Bookings.Events;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyMgmt.Application.Features.Conversations.EventHandler;

public class CreateConversationOnBookingRequested : INotificationHandler<BookingRequestedEvent>
{
    private readonly IApplicationDbContext _context;

    public CreateConversationOnBookingRequested(IApplicationDbContext context) => _context = context;

    public async Task Handle(BookingRequestedEvent notification, CancellationToken cancellationToken)
    {
        var customerId = await _context.Bookings.Where(b => b.Id == notification.BookingId)
            .Select(b => b.UserId)
            .FirstOrDefaultAsync();
        if (customerId == Guid.Empty)
        {
            throw new NotFoundException(nameof(Booking), notification.BookingId);
        }

        var conversation = new Conversation(notification.BookingId, notification.OwnerId, customerId);

        _context.Conversations.Add(conversation);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
