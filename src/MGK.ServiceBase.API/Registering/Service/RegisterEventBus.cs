using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.Registering.Service
{
	/// <summary>
	/// Register services related to the events management.
	/// </summary>
	public class RegisterEventBus : IServiceRegistration
	{
		/// <summary>
		/// Configures services related to the events management through the IServiceCollection.
		/// </summary>
		/// <param name="services">The collection of services.</param>
		/// <param name="configuration">The configuration information.</param>
		public virtual void RegisterServices(IServiceCollection services, IConfiguration configuration)
		{
			//Register here the EventBus and all the things related to events, producers and consumers.
		}
	}
}
