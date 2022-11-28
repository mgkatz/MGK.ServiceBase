using MGK.ServiceBase.DAL.Infrastructure.Factories;
using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MGK.ServiceBase.DAL.Infrastructure.Registering.Service;

public class RegisterFactories : IServiceRegistration
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
    }
}
