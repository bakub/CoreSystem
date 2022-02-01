using NotificationService.Domain.Entities;

namespace NotificationService.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(NotificationHistory notification, string subject, string content);
    }
}
