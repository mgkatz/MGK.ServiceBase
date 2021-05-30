using MGK.ServiceBase.SeedWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace MGK.ServiceBase.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions for the registration of application portions using dependency injection.
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Registers all the base services.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configuration">The application configuration.</param>
        public static void AddBaseAppConfigurations(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.AddAppConfigurationsInAssembly<IStartup>(configuration);
        }

        /// <summary>
        /// Registers all the application portions.
        /// </summary>
        /// <typeparam name="T">The start up application class.</typeparam>
        /// <param name="app">The application builder.</param>
        /// <param name="configuration">The configuration of the application.</param>
        public static void AddAppConfigurationsInAssembly<T>(this IApplicationBuilder app, IConfiguration configuration)
            where T : class
        {
            var customBuilders = typeof(T).Assembly
                .GetTypes()
                .Where(x => typeof(IAppBuilderConfiguration)
                    .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IAppBuilderConfiguration>()
                .ToList();

            customBuilders.ForEach(svc => svc.ConfigureApp(app, configuration));
        }
    }
}
