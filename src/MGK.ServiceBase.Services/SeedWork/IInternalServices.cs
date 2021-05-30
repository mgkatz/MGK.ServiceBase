using AutoMapper;
using Microsoft.Extensions.Logging;

namespace MGK.ServiceBase.Services.SeedWork
{
	public interface IInternalServices<T>
		where T : IService
	{
		IMapper Mapper { get; }
		ILogger<T> Logger { get; }
	}
}
