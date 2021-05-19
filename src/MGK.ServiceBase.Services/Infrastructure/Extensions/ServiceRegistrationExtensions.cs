using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MGK.ServiceBase.Services.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions for the registration of services using dependency injection.
    /// </summary>
    public static class ServiceRegistrationExtensions
    {
        /// <summary>
        /// Registers all the services in an assembly. It is a condition that services must implement the interface IServiceRegistration.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The application configuration.</param>
        public static void AddServicesInAssembly<T>(this IServiceCollection services, IConfiguration configuration)
            where T : class
        {
            var assemblyServices = typeof(T).Assembly
                .GetTypes()
                .Where(x => typeof(IServiceRegistration)
                    .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IServiceRegistration>()
                .ToList();

            assemblyServices.ForEach(svc => svc.RegisterServices(services, configuration));
        }
    }
}
