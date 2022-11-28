using MGK.ServiceBase.Services;

namespace MGK.ServiceBase.Security.Infrastructure.Providers;

public sealed class SecurityServiceProvider : ServiceProvider<string, ISecurityService>, ISecurityServiceProvider
{
    public SecurityServiceProvider(Func<string, ISecurityService> securityServices)
        : base(securityServices)
    {
    }

    public TOutputService Get<TOutputService>()
        where TOutputService : class, ISecurityService
    {
        var key = typeof(TOutputService).Name;
        return Get<TOutputService>(key);
    }
}
