using MGK.Extensions;
using MGK.ServiceBase.Services.Infrastructure.Exceptions;
using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

        public static void AddKeyedServices<T, TKey>(this IServiceCollection services, IDictionary<TKey, Type> servicesToRegister)
            where T : class
            where TKey : notnull
        {
            foreach (var serviceToRegister in servicesToRegister.Values)
			{
                services.AddTransient(serviceToRegister);
			}

            services.AddScoped<Func<TKey, T>>(provider => (key) =>
            {
                if (servicesToRegister.ContainsKey(key))
                {
                    return provider.GetRequiredService(servicesToRegister[key]) as T;
				}
                else
                {
                    throw new ServiceRegistrationException(
                        ServicesResources.MessagesResources.ErrorRegisteringTitle,
                        ServicesResources.MessagesResources.ErrorRegisteringMessage.Format(key));
                }
            });
        }
	}
}
