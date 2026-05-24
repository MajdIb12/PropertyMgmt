using MediatR;
using PropertyMgmt.Application.Features.Bookings.Events;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;
using PropertyMgmt.Domain.Enums;

namespace PropertyMgmt.Application.Features.Notifications.EventHandler;

public class BookingRequestedEventHandler(IApplicationDbContext context, INotificationService notificationService) : INotificationHandler<BookingRequestedEvent>
{

    public async Task Handle(BookingRequestedEvent notification, CancellationToken cancellationToken)
    {
        var title = "طلب حجز جديد! 🔔";
        var message = $"قام المستأجر {notification.GuestName} بطلب حجز لعقارك ({notification.PropertyTitle}) وبانتظار موافقتك.";

        var dbNotification = new Notification
        {
            Id = Guid.NewGuid(),
            Title = title,
            Message = message,
            UserId = notification.OwnerId,
            IsRead = false,
            Type = NotificationType.Booking,
            TenantId = notification.TenantId
        };

        context.Notifications.Add(dbNotification);
        await context.SaveChangesAsync(cancellationToken);

        // 🚀 الخطوة الثانية: إرسال الإشعار فوراً في الوقت الفعلي عبر الشبكة
        await notificationService.SendRealTimeNotificationAsync(
            notification.OwnerId,
            title,
            message,
            notification.TenantId
        );
    }
}