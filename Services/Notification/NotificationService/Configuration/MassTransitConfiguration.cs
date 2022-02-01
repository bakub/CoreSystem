using GreenPipes;
using MassTransit;
using MassTransit.MultiBus;
using NotificationService.Consumers.CreateUser;

namespace NotificationService.Configuration
{
    public static class AddMassTransitRegister
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateUserConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(configuration["RabbitMq:Host"]), h =>
                            {
                                h.Username(configuration["Username"]);
                                h.Password(configuration["Password"]);
                            });
                    cfg.ReceiveEndpoint("create-user", e =>
                    {
                        e.PrefetchCount = 16;
                        e.UseMessageRetry(r => r.Interval(2, 100));
                        e.ConfigureConsumer<CreateUserConsumer>(context);
                    });
                });
            });
            services.AddMassTransitHostedService();

            return services;
        }
    }
}
