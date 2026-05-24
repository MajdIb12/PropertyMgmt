using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Interfaces;

public interface INotificationService
{
    Task SendRealTimeNotificationAsync(Guid userId, string title, string message, string tenantId);
}
