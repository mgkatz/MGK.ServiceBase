using MGK.ServiceBase.Services.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.Messaging.Infrastructure.Extensions;

public static class ServiceRegistrationExtensions
{
    public static void AddMessagingServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServicesInAssembly<IEmailClient>(configuration);
    }
}
