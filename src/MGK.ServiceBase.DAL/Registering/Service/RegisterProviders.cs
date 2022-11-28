using MGK.ServiceBase.DAL.Infrastructure.Providers;
using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MGK.ServiceBase.DAL.Infrastructure.Registering.Service;

public class RegisterProviders : IServiceRegistration
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddScoped<IDataAccessServiceProvider, DataAccessServiceProvider>();
    }
}
