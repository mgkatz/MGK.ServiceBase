using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MGK.ServiceBase.DAL.Infrastructure.Extensions
{
	public static class MigrationsExtensions
	{
        public static void Run<TContext>(this IServiceProvider serviceProvider)
            where TContext : DbContext
        {
            using var context = serviceProvider.GetRequiredService<TContext>();
            context.Database.Migrate();
        }
    }
}
