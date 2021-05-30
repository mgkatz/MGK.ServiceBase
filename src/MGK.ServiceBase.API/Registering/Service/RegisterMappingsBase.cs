using AutoMapper;
using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.Registering.Service
{
	/// <summary>
	/// Allows to implement a way for registering the mappings.
	/// </summary>
	public abstract class RegisterMappingsBase : IServiceRegistration
	{
		/// <summary>
		/// Registers the mappings.
		/// </summary>
		/// <param name="services">The collection of services.</param>
		/// <param name="configuration">The configuration information.</param>
		public virtual void RegisterServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(this.CreateMapper());
		}

		/// <summary>
		/// Allows to implement a way to create the mapper configuration and add the mappings.
		/// </summary>
		/// <returns>The mapper.</returns>
		protected abstract IMapper CreateMapper();
	}
}
