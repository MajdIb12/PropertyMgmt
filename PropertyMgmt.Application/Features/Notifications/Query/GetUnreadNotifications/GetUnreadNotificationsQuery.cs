using MediatR;

namespace PropertyMgmt.Application.Features.Notifications.Query.GetUnreadNotifications;

public record GetUnreadNotificationsQuery(Guid UserId) : IRequest<ICollection<NotificationDto>>;
