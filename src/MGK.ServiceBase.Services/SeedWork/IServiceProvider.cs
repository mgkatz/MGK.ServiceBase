namespace MGK.ServiceBase.Services.SeedWork
{
	public interface IServiceProvider<TKey, TService>
		where TKey : notnull
		where TService: IService
	{
		TOutputService Get<TOutputService>(TKey key)
			where TOutputService : class, IService;
	}
}
