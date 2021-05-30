using AutoMapper;
using MGK.ServiceBase.CQRS.SeedWork;
using MGK.ServiceBase.Services;
using Microsoft.Extensions.Logging;

namespace MGK.ServiceBase.CQRS
{
	internal class CqrsInternalServices<T> : InternalServices<T>, ICqrsInternalServices<T>
		where T : class, IHandlerService
	{
		public CqrsInternalServices(
			IMapper mapper,
			ILogger<T> logger)
			: base(mapper, logger)
		{
		}
	}
}
