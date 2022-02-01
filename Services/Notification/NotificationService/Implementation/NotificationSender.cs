using NotificationService.Domain.Entities;
using NotificationService.Domain.Repository;
using NotificationService.Interfaces;

namespace NotificationService.Implementation
{
    public class NotificationSender : INotificationSender
    {
        private readonly INotificationRepository _emailRepository;
        private readonly IEmailService _emailSenderService;

        public NotificationSender(INotificationRepository emailRepository, IEmailService emailSenderService)
        {
            _emailRepository = emailRepository;
            _emailSenderService = emailSenderService;
        }

        public async Task SendPendingNotifications()
        {
            var pendingNotifications = await _emailRepository.GetPendingNotifications();
            foreach (var notification in pendingNotifications)
            {
                await TrySendEmail(notification);
                await _emailRepository.MarkNotificationAsSent(notification);
            }
        }

        private async Task TrySendEmail(NotificationHistory notification)
        {
            try
            {
                await _emailSenderService.SendEmail(notification, "sujbject", "content");
            }
            catch (Exception ex)
            {
                await _emailRepository.IncreaseNotificationErrorCount(notification);
            }
        }
    }
}
