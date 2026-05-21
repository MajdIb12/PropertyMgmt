using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Interfaces;

public interface INotificationService
{
    Task<ICollection<Notification>> GetUnreadNotificationsAsync(Guid userId, string tenantId);

    // دالة لإرسال إشعار حيّ لمستعمل محدد داخل مستأجر (Tenant) محدد
    Task SendRealTimeNotificationAsync(Guid userId, string title, string message, string tenantId);
}