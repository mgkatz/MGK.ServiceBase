using MGK.ServiceBase.Services.Infrastructure.Exceptions;

namespace MGK.ServiceBase.Services;

public abstract class ServiceProvider<TKey, TService> : IServiceProvider<TKey, TService>
	where TKey : notnull
	where TService : IService
{
	protected ServiceProvider(Func<TKey, TService> services)
        : this(new Func<TKey, TService>[] { services })
	{
    }

    protected ServiceProvider(IEnumerable<Func<TKey, TService>> services)
    {
        Services = services;
    }

    protected IEnumerable<Func<TKey, TService>> Services { get; }

	public virtual TOutputService Get<TOutputService>(TKey key)
		where TOutputService : class, IService
	{
        TOutputService service = null;

        foreach (var listOfServices in Services)
        {
            service = listOfServices(key) as TOutputService;

            if (service is not null) break;
        }

        if (service is null)
        {
            Raise.Error.Generic<ServiceRegistrationException>(
                ServicesResources.MessagesResources.ErrorRegisteringTitle,
                ServicesResources.MessagesResources.ErrorRegisteringMessage.Format(key.ToString()));
        }

        return service;
    }
}
