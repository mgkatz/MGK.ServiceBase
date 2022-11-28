using MediatR;

namespace MGK.ServiceBase.API.SeedWork;

public interface IControllerInternalServices<T> : IInternalServices<T>
	where T : IControllerService
{
	IMediator Mediator { get; }
}
