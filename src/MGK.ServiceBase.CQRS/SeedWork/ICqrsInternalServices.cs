using MGK.ServiceBase.Services.SeedWork;

namespace MGK.ServiceBase.CQRS.SeedWork
{
	public interface ICqrsInternalServices<T> : IInternalServices<T>
		where T : IHandlerService
	{
	}
}
