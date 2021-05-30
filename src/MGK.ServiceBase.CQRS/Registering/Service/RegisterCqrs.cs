using MGK.ServiceBase.CQRS.SeedWork;
using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.CQRS.Registering.Service
{
	public class RegisterCqrs : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(ICqrsInternalServices<>), typeof(CqrsInternalServices<>));
        }
    }
}
