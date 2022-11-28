using MGK.ServiceBase.Services.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.Security.Infrastructure.Extensions;

public static class ServiceRegistrationExtensions
{
    public static void AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServicesInAssembly<ISecurityService>(configuration);
    }
}
