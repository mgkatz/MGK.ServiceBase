using MGK.ServiceBase.Configuration;
using MGK.ServiceBase.Configuration.SeedWork;
using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.Registering.Service
{
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
			services.AddScoped<IMultiTenantSetup, MultiTenantSetup>();
			services.AddScoped<IConfigurationSetup, ConfigurationSetup>();
		}
	}
}
