using NotificationService.Domain.Repository;
using NotificationService.Implementation;
using NotificationService.Interfaces;

namespace NotificationService.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<INotificationSender, NotificationSender>();

            services.AddTransient<IEmailService, Implementation.EmailService>();

            return services;
        }
    }
}
