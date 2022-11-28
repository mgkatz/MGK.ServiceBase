using MGK.ServiceBase.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MGK.ServiceBase.API.Registering.Service;

/// <summary>
/// Register services related to the cache management.
/// </summary>
public class RegisterCacheManagement : IServiceRegistration
{
    /// <summary>
    /// Configures services related to the cache management through the IServiceCollection.
    /// </summary>
    /// <param name="services">The collection of services.</param>
    /// <param name="configuration">The configuration information.</param>
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddSingleton<IMemoryCacheManager, MemoryCacheManager>();
    }
}
