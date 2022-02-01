using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Utils.Extensions
{
    public static class MigrateDatabaseExtension
    {
        public static IApplicationBuilder MigrateDatabaseOnStartUp<T>(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<T>() as DbContext;
                context.Database.Migrate();
            }

            return app;
        }
    }
}
