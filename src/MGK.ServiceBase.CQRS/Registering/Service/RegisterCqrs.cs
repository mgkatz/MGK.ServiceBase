using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MGK.ServiceBase.CQRS.Registering.Service;

	public class RegisterCqrs : IServiceRegistration
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddScoped(typeof(ICqrsInternalServices<>), typeof(CqrsInternalServices<>));
    }
}
