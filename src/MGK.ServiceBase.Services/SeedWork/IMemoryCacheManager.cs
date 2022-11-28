using MGK.Extensions.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace MGK.ServiceBase.Services.SeedWork;

public interface IMemoryCacheManager : ICacheManager
{
    MemoryCacheEntryOptions GetCacheEntryOptions(
        DateTimeInterval interval = DateTimeInterval.Minutes,
        double value = 30);
}
