using MGK.ServiceBase.Security.Infrastructure.Options;
using MGK.ServiceBase.Services.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace MGK.ServiceBase.Security.Registering.Service;

public class RegisterSecurity : IServiceRegistration
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtAuthOptions>(configuration.GetSection(JwtAuthOptions.OptionsKey));
        AddSecurityServices(services);
    }

    private static void AddSecurityServices(IServiceCollection services)
    {
        var securityServices = new Dictionary<string, Type>()
        {
            { nameof(IJwtService), typeof(JwtService) }
        };

        services.AddKeyedServices<ISecurityService, string>(securityServices);
    }
}
