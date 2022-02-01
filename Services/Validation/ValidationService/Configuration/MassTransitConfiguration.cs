using GreenPipes;
using MassTransit;
using MassTransit.MultiBus;
using ValidationService.Consumers;

namespace ValidationService.Configuration
{
    public static class MassTransitConfiguration
    {
        public static IServiceCollection AddMassTransitRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ValidateUserEmailAddressConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(configuration["RabbitMq:Host"]), h =>
                    {
                        h.Username(configuration["Username"]);
                        h.Password(configuration["Password"]);
                    });
                    cfg.ReceiveEndpoint("validate-user-email-address", e =>
                    {
                        e.PrefetchCount = 16;
                        e.UseMessageRetry(r => r.Interval(2, 100));
                        e.ConfigureConsumer<ValidateUserEmailAddressConsumer>(context);
                    });
                });
            });
            services.AddMassTransitHostedService();

            return services;
        }
    }
}
