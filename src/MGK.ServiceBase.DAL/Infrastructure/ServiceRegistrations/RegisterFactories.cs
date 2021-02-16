using MGK.ServiceBase.SeedWork;
using MGK.ServiceBase.DAL.Infrastructure.Factories;
using MGK.ServiceBase.DAL.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.DAL.Infrastructure.ServiceRegistrations
{
    public class RegisterFactories : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        }
    }
}
