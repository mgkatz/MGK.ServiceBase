using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.DAL.Infrastructure.Extensions;

public static class MigrationsExtensions
{
    public static void RunDbMigrations<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.Run<TContext>();
    }

    public static void Run<TContext>(this IServiceProvider serviceProvider)
        where TContext : DbContext
    {
        using var context = serviceProvider.GetRequiredService<TContext>();
        context.Database.Migrate();
    }
}
