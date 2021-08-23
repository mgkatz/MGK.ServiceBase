using MGK.ServiceBase.API;
using MGK.ServiceBase.SeedWork;
using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.Registering.Service
{
	public class RegisterApiServices : IServiceRegistration
	{
		public void RegisterServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped(typeof(IControllerInternalServices<>), typeof(ControllerInternalServices<>));
		}
	}
}
