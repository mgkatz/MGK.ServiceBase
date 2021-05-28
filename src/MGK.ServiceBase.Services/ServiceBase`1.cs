using AutoMapper;
using MGK.Acceptance;
using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Logging;

namespace MGK.ServiceBase.Services
{
	public abstract class ServiceBase<T>
		where T : class, IService
	{
		protected ServiceBase(IInternalServices<T> internalServices)
		{
			Ensure.Parameter.IsNotNull(internalServices, nameof(internalServices));
			Ensure.Value.IsNotNull(internalServices.Logger, nameof(internalServices.Logger));
			Ensure.Value.IsNotNull(internalServices.Mapper, nameof(internalServices.Mapper));

			Logger = internalServices.Logger;
			Mapper = internalServices.Mapper;
		}

		protected ILogger<T> Logger { get; }

		protected IMapper Mapper { get; }
	}
}
