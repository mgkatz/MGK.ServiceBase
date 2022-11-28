using AutoMapper;
using Microsoft.Extensions.Logging;

namespace MGK.ServiceBase.Services;

public abstract class ServiceHandler<T>
	where T : class, IService
{
	protected ServiceHandler(IInternalServices<T> internalServices)
	{
		Ensure.Parameter.IsNotNull(internalServices, nameof(internalServices));

		Logger = internalServices.Logger;
		Mapper = internalServices.Mapper;
	}

	protected ILogger<T> Logger { get; }

	protected IMapper Mapper { get; }
}
