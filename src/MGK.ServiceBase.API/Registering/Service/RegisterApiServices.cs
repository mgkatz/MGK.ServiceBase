using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MGK.ServiceBase.API.Registering.Service;

public class RegisterApiServices : IServiceRegistration
{
	public void RegisterServices(IServiceCollection services, IConfiguration configuration)
	{
		services.TryAddScoped(typeof(IControllerInternalServices<>), typeof(ControllerInternalServices<>));
#if DEBUG
        services.TryAddSingleton(services);
#endif
    }
}
