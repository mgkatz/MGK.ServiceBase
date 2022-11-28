using MGK.ServiceBase.Configuration;
using MGK.ServiceBase.Configuration.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MGK.ServiceBase.API.Registering.Service;

/// <summary>
/// Register services related to the application configuration.
/// </summary>
public class RegisterConfiguration : IServiceRegistration
{
	/// <summary>
	/// Configures services related to the application configuration through the IServiceCollection.
	/// </summary>
	/// <param name="services">The collection of services.</param>
	/// <param name="configuration">The configuration information.</param>
	public virtual void RegisterServices(IServiceCollection services, IConfiguration configuration)
	{
		services.TryAddScoped<IMultiTenantSetup, MultiTenantSetup>();
		services.TryAddScoped<IConfigurationSetup, ConfigurationSetup>();
	}
}
