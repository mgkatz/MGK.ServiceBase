using MediatR;
using MGK.ServiceBase.API.Infrastructure.Exceptions;
using MGK.ServiceBase.Configuration.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.API.Registering.Service;

/// <summary>
/// Allows to register the services related to the publishing of messages.
/// </summary>
public class RegisterMediatR : IServiceRegistration
{
	/// <summary>
	/// Register the services related to the publishing of messages.
	/// </summary>
	/// <param name="services">The collection of services.</param>
	/// <param name="configuration">The configuration information.</param>
	public virtual void RegisterServices(IServiceCollection services, IConfiguration configuration)
	{
		var serviceProvider = services.BuildServiceProvider();
		var serviceParameters = serviceProvider.GetService<IMicroServiceParameters>();

        Ensure.Value.IsNotNull<ApplicationValidationException>(
            serviceParameters,
            BaseResources.MessagesResources.ErrorSwaggerRegistrationDescription);
        
		// Scan assemblies and add handlers, preprocessors, and postprocessors implementations to the container.
        services.AddMediatR(serviceParameters.ApiStartup.Assembly);

		serviceProvider.Dispose();
	}
}
