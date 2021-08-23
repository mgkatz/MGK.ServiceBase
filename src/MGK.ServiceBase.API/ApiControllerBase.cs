using AutoMapper;
using MediatR;
using MGK.Acceptance;
using MGK.ServiceBase.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MGK.ServiceBase.API
{
	public abstract class ApiControllerBase<T> : ControllerBase
		where T : class, IControllerService
	{
		protected ApiControllerBase(IControllerInternalServices<T> controllerInternalServices)
		{
			Ensure.Parameter.IsNotNull(controllerInternalServices, nameof(controllerInternalServices));

			Logger = controllerInternalServices.Logger;
			Mapper = controllerInternalServices.Mapper;
			Mediator = controllerInternalServices.Mediator;
		}

		protected ILogger<T> Logger { get; }

		protected IMapper Mapper { get; }

		protected IMediator Mediator { get; }
	}
}
