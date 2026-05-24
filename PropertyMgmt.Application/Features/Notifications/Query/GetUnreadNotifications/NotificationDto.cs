using PropertyMgmt.Domain.Enums;

namespace PropertyMgmt.Application.Features.Notifications.Query.GetUnreadNotifications
{
    public class NotificationDto
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}