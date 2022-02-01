using Integrations.Interfaces;
using MassTransit;
using NotificationService.Domain.Enums;
using NotificationService.Domain.Repository;

namespace NotificationService.Consumers.CreateUser
{
    public class CreateUserConsumer : IConsumer<CreateUserEvent>
    {
        private readonly INotificationRepository _notificationRepository;

        public CreateUserConsumer(INotificationRepository emailRepository)
        {
            _notificationRepository = emailRepository;
        }

        public async Task Consume(ConsumeContext<CreateUserEvent> context)
        {
            var createUserEvent = context.Message;

            await _notificationRepository.CreateNotification(
                createUserEvent.EmailAddress, 
                NotificationType.REGISTRATION
                );
        }
    }
}
