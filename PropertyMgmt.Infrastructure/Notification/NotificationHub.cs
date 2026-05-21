using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PropertyMgmt.Infrastructure.Notifications;

public class NotificationHub : Hub
{
    private readonly ITenantService _tenantService;
    private readonly INotificationService _notificationService;

    public NotificationHub(ITenantService tenantService, INotificationService notificationService)
    {
        _tenantService = tenantService;
        _notificationService = notificationService;

    }

    // 🚀 يتم استدعاء هذه الدالة تلقائياً عندما يفتح المتصفح (العميل) الاتصال
    public override async Task OnConnectedAsync()
    {
        var tenantId = _tenantService.TenantId;

        // جلب الـ UserId من الـ Claims الخاصة بالمستخدم الحالي الموثق
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrEmpty(tenantId) && !string.IsNullOrEmpty(userId))
        {
            // 🔒 تأمين خارق: نضع المستخدم في جروب خاص يجمع بين الـ Tenant والـ User
            // مثل: Tenant-شركة_أحمد-User-مجد
            string privateGroup = $"Tenant-{tenantId}-User-{userId}";
            await Groups.AddToGroupAsync(Context.ConnectionId, privateGroup);

            // (اختياري) يمكن أيضاً وضعه في جروب عام للـ Tenant بالكامل لإرسال إعلانات الشركة لاحقاً
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Tenant-{tenantId}");

            var unreadNotifications = await _notificationService.GetUnreadNotificationsAsync(Guid.Parse(userId), tenantId);

            if (unreadNotifications != null && unreadNotifications.Any())
            {
                // 🚀 السحر هنا: نستخدم Clients.Caller لإرسال القائمة لهذا الجهاز المتصل توّاً فقط!
                // سيتلقى الـ Frontend مصفوفة (Array) تحتوي على كل الإشعارات القديمة ليعرضها في جرس التنبيهات
                await Clients.Caller.SendAsync("ReceiveUnreadNotifications", unreadNotifications);
            }
        }

        await base.OnConnectedAsync();
    }

    // 🧹 يتم استدعاؤها تلقائياً عند إغلاق المستخدم للموقع
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var tenantId = _tenantService.TenantId;
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrEmpty(tenantId) && !string.IsNullOrEmpty(userId))
        {
            string privateGroup = $"Tenant-{tenantId}-User-{userId}";
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, privateGroup);
        }

        await base.OnDisconnectedAsync(exception);
    }
}
