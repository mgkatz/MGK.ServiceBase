using MGK.ServiceBase.DAL.Infrastructure.Factories;
using MGK.ServiceBase.DAL.SeedWork;
using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.DAL.Infrastructure.Registering.Service
{
    public class RegisterFactories : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        }
    }
}
