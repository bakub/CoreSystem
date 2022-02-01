using Integrations.Interfaces;
using MassTransit;
using MassTransit.MultiBus;
using UserService.Consumers.CreateUser;
using UserService.Consumers.GetUsers;

namespace UserService.Configuration
{
    public static class MassTransitConfiguration
    {
        public static IServiceCollection AddMassTransitRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediator(x =>
            {
                x.AddConsumer<GetUserByIdConsumer>();
                x.AddRequestClient<GetUserByIdQuery>();

                x.AddConsumer<GetUsersConsumer>();
                x.AddRequestClient<GetUsersQuery>();

                x.AddConsumer<CreateUserConsumer>();
                x.AddRequestClient<CreateUserCommand>();

            });
            services.AddMassTransit(x =>
            {
                x.AddRequestClient<ValidateUserEmailAddressMessage>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(configuration["RabbitMq:Host"]), h =>
                    {
                        h.Username(configuration["Username"]);
                        h.Password(configuration["Password"]);
                    });

                });
            });

            return services;
        }
    }
}
