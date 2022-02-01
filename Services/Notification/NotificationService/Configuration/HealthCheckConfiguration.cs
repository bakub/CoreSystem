using Hangfire;
using Hangfire.MemoryStorage;
using NotificationService.Interfaces;

namespace NotificationService.Configuration
{
    public static class HealthCheckConfiguration
    {
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddRabbitMQ(rabbitConnectionString: new Uri(configuration["RabbitMq:Host"]))
                .AddSqlServer(configuration["SqlServer:ConnectionString"]);

            return services;
        }

    }
}
