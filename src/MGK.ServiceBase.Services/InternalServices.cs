using AutoMapper;
using MGK.Acceptance;
using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Logging;

namespace MGK.ServiceBase.Services
{
	public abstract class InternalServices<T> : IInternalServices<T>
		where T : class, IService
	{
		protected InternalServices(
			IMapper mapper,
			ILogger<T> logger)
		{
			Ensure.Parameter.IsNotNull(mapper, nameof(mapper));
			Ensure.Parameter.IsNotNull(logger, nameof(logger));

			Logger = logger;
			Mapper = mapper;
		}

		public ILogger<T> Logger { get; }

		public IMapper Mapper { get; }
	}
}
