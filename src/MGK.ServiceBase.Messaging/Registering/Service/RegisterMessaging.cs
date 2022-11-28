using MGK.ServiceBase.Messaging.Infrastructure.Options;
using MGK.ServiceBase.Messaging.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MGK.ServiceBase.Messaging.Registering.Service;

public class RegisterMessaging : IServiceRegistration
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpOptions>(configuration.GetSection(SmtpOptions.OptionsKey));
        services.TryAddScoped<IEmailClient, MailClient>();
        services.TryAddSingleton<ISmtpObject, SmtpObject>();
    }
}
