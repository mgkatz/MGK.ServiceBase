using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace MGK.ServiceBase.SeedWork
{
	/// <summary>
	/// Allows to implement a generic way to configure the applications on the API's Startup.
	/// </summary>
	public interface IAppBuilderConfiguration
	{
		/// <summary>
		/// Configures an application through the IApplicationBuilder.
		/// </summary>
		/// <param name="app">The IApplicationBuilder.</param>
		/// <param name="configuration">The configuration information.</param>
		void ConfigureApp(IApplicationBuilder app, IConfiguration configuration);
	}
}
