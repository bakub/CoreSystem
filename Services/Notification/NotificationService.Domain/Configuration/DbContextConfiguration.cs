using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Domain.Context;

namespace NotificationService.Domain.Configuration
{
    public static class DbContextConfiguration
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NotificationsDbContext>(options =>
                options.UseSqlServer(configuration["SqlServer:ConnectionString"]));
            //builder.Services.AddDbContext<NotificationsDbContext>(options => options.UseInMemoryDatabase("EmailDb"));

            return services;
        }
    }
}
