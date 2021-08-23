using AutoMapper;
using MediatR;
using MGK.Acceptance;
using MGK.ServiceBase.SeedWork;
using MGK.ServiceBase.Services;
using Microsoft.Extensions.Logging;

namespace MGK.ServiceBase.API
{
	internal class ControllerInternalServices<T> : InternalServices<T>, IControllerInternalServices<T>
		where T : class, IControllerService
	{
		public ControllerInternalServices(
			IMediator mediator,
			IMapper mapper,
			ILogger<T> logger)
			: base(mapper, logger)
		{
			Ensure.Parameter.IsNotNull(mediator, nameof(mediator));

			Mediator = mediator;
		}

		public IMediator Mediator { get; }
	}
}
