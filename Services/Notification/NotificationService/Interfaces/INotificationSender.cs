namespace NotificationService.Interfaces
{
    public interface INotificationSender
    {
        Task SendPendingNotifications();
    }
}
