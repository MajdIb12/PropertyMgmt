namespace PropertyMgmt.Api.Hubs; // 📍 مكاني الجديد في طبقة الـ API

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PropertyMgmt.Application.Features.Notifications.Query.GetUnreadNotifications;
using PropertyMgmt.Application.Interfaces;
using System.Security.Claims;

[Authorize]
public class NotificationHub : Hub
{
    private readonly IMediator _mediator;
    private readonly ITenantService _tenantService; // نحتاجها فقط لمعرفة الجروب عند الاتصال

    public NotificationHub(IMediator mediator, ITenantService tenantService)
    {
        _mediator = mediator;
        _tenantService = tenantService;
    }

    public override async Task OnConnectedAsync()
    {
        var tenantId = _tenantService.TenantId;
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrEmpty(tenantId) && !string.IsNullOrEmpty(userId))
        {
            // 1️⃣ تأمين المستخدم داخل الـ Group الخاص به
            string privateGroup = $"Tenant-{tenantId}-User-{userId}";
            await Groups.AddToGroupAsync(Context.ConnectionId, privateGroup);

            // 2️⃣ 🧠 السحر هنا: نرسل Query عبر MediatR لجلب الإشعارات غير المقروءة
            // الـ Handler في طبقة الـ Application هو من سيذهب للداتابيز ويجلبها
            var unreadNotifications = await _mediator.Send(new GetUnreadNotificationsQuery());

            if (unreadNotifications != null && unreadNotifications.Any())
            {
                // 3️⃣ ضخ الإشعارات القديمة للجهاز الذي اتصل للتو
                await Clients.Caller.SendAsync("ReceiveUnreadNotifications", unreadNotifications);
            }
        }

        await base.OnConnectedAsync();
    }

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