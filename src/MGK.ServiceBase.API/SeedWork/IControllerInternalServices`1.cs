using MediatR;
using MGK.ServiceBase.Services.SeedWork;

namespace MGK.ServiceBase.SeedWork
{
	public interface IControllerInternalServices<T> : IInternalServices<T>
		where T : IControllerService
	{
		IMediator Mediator { get; }
	}
}
