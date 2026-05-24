using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Notifications.Query.GetUnreadNotifications;

public class GetUnreadNotificationsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetUnreadNotificationsQuery, ICollection<NotificationDto>>
{
    public async Task<ICollection<NotificationDto>> Handle(GetUnreadNotificationsQuery request, CancellationToken cancellationToken)
    {
        return await context.Notifications.Where(n => n.UserId == request.UserId && !n.IsRead)
            .Select(n => new NotificationDto
            {
                Title = n.Title,
                Message = n.Message,
                Type = n.Type.ToString()
            }).ToListAsync();
    }
}
