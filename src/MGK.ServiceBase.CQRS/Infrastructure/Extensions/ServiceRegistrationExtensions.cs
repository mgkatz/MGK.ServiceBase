using MGK.ServiceBase.CQRS.SeedWork;
using MGK.ServiceBase.Services.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.CQRS.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions for the registration of services using dependency injection.
    /// </summary>
    public static class ServiceRegistrationExtensions
    {
        /// <summary>
        /// Registers all the base services.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The application configuration.</param>
        public static void AddCqrsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServicesInAssembly<IHandlerService>(configuration);
        }
    }
}
