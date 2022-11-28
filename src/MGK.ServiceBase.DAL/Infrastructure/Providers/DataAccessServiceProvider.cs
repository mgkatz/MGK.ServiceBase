using MGK.ServiceBase.Services;

namespace MGK.ServiceBase.DAL.Infrastructure.Providers;

public sealed class DataAccessServiceProvider :
	ServiceProvider<string, IDataAccessService>,
	IDataAccessServiceProvider
{
    public DataAccessServiceProvider(IEnumerable<Func<string, IDataAccessService>> dataAccessServices)
        : base(dataAccessServices)
	{
	}

	public TOutputService Get<TOutputService>()
		where TOutputService : class, IDataAccessService
	{
		var key = typeof(TOutputService).Name;
		return Get<TOutputService>(key);
	}
}
