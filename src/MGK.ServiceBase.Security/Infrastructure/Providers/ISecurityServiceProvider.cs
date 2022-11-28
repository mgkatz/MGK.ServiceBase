namespace MGK.ServiceBase.Security.Infrastructure.Providers;

public interface ISecurityServiceProvider : IServiceProvider<string, ISecurityService>
{
    TOutputService Get<TOutputService>()
        where TOutputService : class, ISecurityService;
}
