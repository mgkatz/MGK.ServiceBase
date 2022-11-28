using MGK.ServiceBase.Services.SeedWork;

namespace MGK.ServiceBase.DAL.Infrastructure.Providers;

public interface IDataAccessServiceProvider : IServiceProvider<string, IDataAccessService>
{
	TOutputService Get<TOutputService>()
		where TOutputService : class, IDataAccessService;
}
