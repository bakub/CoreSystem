using Hangfire;
using Hangfire.MemoryStorage;
using NotificationService.Interfaces;

namespace NotificationService.Configuration
{
    public static class HangfireConfiguration
    {
        public static IServiceCollection AddHangfire(this IServiceCollection services)
        {
            services.AddHangfire(configuration => {
                configuration.UseMemoryStorage();
            });

            services.AddHangfireServer();

            return services;
        }

        public static IApplicationBuilder UseHangfireJobs(this IApplicationBuilder app)
        {
            StartNotificationService();

            return app;
        }

        private static void StartNotificationService()
        {
            RecurringJob.AddOrUpdate<INotificationSender>(x => x.SendPendingNotifications(), "0 */1 * * * *");
        }


    }
}
