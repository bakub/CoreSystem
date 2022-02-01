using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Gateway.Configuration
{
    public static class AddHealthCheckDI
    {
        private const string HealthUi = "/api/dashboard/health";

        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks();
                    //.AddRabbitMQ(rabbitConnectionString: new Uri(configuration["RabbitMq:Host"]))
                    //.AddSqlServer(configuration["SqlServer:ConnectionString"])
                    //.AddNpgSql(configuration["PostgreSQL:ConnectionString"]);
            
            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.AddHealthCheckEndpoint("Users service", "http://userservice/hc");
                setup.AddHealthCheckEndpoint("Notification service", "http://notificationservice/hc");
                setup.AddHealthCheckEndpoint("Validation service", "http://validationservice/hc");
                //setup.AddHealthCheckEndpoint("Forms service", "http://formservice/hc");
                setup.AddHealthCheckEndpoint("Gateway", "http://localhost/this-hc");
            }).AddInMemoryStorage();

            return services;
        }

        public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/this-hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI(options =>
            {
                options.UIPath = HealthUi;
                options.ApiPath = HealthUi + "/api";
                options.ResourcesPath = HealthUi + "/resources";
                options.WebhookPath = HealthUi + "/webhooks";

                options.UseRelativeApiPath = false;
                options.UseRelativeResourcesPath = false;
                options.UseRelativeWebhookPath = false;
            });

            return app;
        }
    }
}
