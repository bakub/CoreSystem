using NotificationService.Domain.Entities;
using NotificationService.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationService.Domain.Repository
{
    public interface INotificationRepository
    {
        Task<NotificationHistory> GetNotificationById(int id);
        Task<ICollection<NotificationHistory>> GetPendingNotifications();
        Task<long> CreateNotification(string email, NotificationType type);
        Task MarkNotificationAsSent(NotificationHistory notification);
        Task IncreaseNotificationErrorCount(NotificationHistory notification);
    }
}
