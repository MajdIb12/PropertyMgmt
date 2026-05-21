using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.Notifications;

public class SignalRNotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IApplicationDbContext _context;

    public SignalRNotificationService(IHubContext<NotificationHub> hubContext, IApplicationDbContext context)
    {
        _hubContext = hubContext;
        _context = context;
    }

    public async Task<ICollection<Notification>> GetUnreadNotificationsAsync(Guid userId, string tenantId)
    {
        var notifications = await _context.Notifications.AsNoTracking()
            .Where(n => n.UserId == userId && n.TenantId == tenantId && !n.IsRead)
            .ToListAsync();
        return notifications;
    }

    public async Task SendRealTimeNotificationAsync(Guid userId, string title, string message, string tenantId)
    {
        // 🎯 تحديد الغرفة المستهدفة بدقة تامة بناءً على الـ Tenant والـ User القادمين من الحدث
        string targetGroup = $"Tenant-{tenantId}-User-{userId}";

        // 📡 بث الإشعار الحي! العميل سيستمع لدالة اسمها "ReceiveNotification"
        await _hubContext.Clients.Group(targetGroup).SendAsync("ReceiveNotification", new
        {
            Title = title,
            Message = message,
            Timestamp = DateTime.UtcNow
        });
    }
}