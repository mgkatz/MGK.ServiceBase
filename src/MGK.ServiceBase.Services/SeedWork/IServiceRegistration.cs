using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.Services.SeedWork
{
	/// <summary>
	/// Allows to implement a generic way to register services on the API's Startup.
	/// </summary>
	public interface IServiceRegistration
	{
		/// <summary>
		/// Configures services through the IServiceCollection.
		/// </summary>
		/// <param name="services">The IServiceCollection.</param>
		/// <param name="configuration">The configuration information.</param>
		void RegisterServices(IServiceCollection services, IConfiguration configuration);
	}
}
