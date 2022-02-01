using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain.Context;

namespace UserService.Domain.Configuration
{
    public static class DbContextConfiguration
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(configuration["PostgreSQL:ConnectionString"]));

            return services;
        }
    }
}
