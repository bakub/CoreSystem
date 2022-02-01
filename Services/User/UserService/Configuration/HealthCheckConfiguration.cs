using UserService.Configuration;

namespace UserService.Configuration
{
    public static class HealthCheckConfiguration
    {
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddRabbitMQ(rabbitConnectionString: new Uri(configuration["RabbitMq:Host"]))
                .AddNpgSql(configuration["PostgreSQL:ConnectionString"]);

            return services;
        }

    }
}
