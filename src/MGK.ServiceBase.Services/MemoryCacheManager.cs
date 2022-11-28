using MGK.Extensions.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace MGK.ServiceBase.Services;

public class MemoryCacheManager : IMemoryCacheManager
{
    private readonly IMemoryCache _memoryCache;
    private const DateTimeInterval DefaultInterval = DateTimeInterval.Minutes;
    private const double DefaultIntervalValue = 30;

    public MemoryCacheManager(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public virtual void AddToCache<T>(
        object key,
        T valueToCache,
        DateTimeInterval interval = DefaultInterval,
        double intervalValue = DefaultIntervalValue)
        where T : class
    {
        var cacheEntryOptions = GetCacheEntryOptions(interval, intervalValue);
        _memoryCache.Set(key, valueToCache, cacheEntryOptions);
    }

    public virtual void AddToCache<T>(
        object key,
        IEnumerable<T> valueToCache,
        DateTimeInterval interval = DefaultInterval,
        double intervalValue = DefaultIntervalValue)
        where T : class
    {
        var cacheEntryOptions = GetCacheEntryOptions(interval, intervalValue);
        _memoryCache.Set(key, valueToCache, cacheEntryOptions);
    }

    public virtual MemoryCacheEntryOptions GetCacheEntryOptions(
        DateTimeInterval interval = DefaultInterval,
        double value = DefaultIntervalValue)
    {
        if (!TryFromAnyInterval(interval, value, out var timeSpan))
            timeSpan = TimeSpan.FromMinutes(DefaultIntervalValue);

        return new MemoryCacheEntryOptions().SetSlidingExpiration(timeSpan);
    }

    public bool TryGetValue(object key, out object value) =>
        _memoryCache.TryGetValue(key, out value);

    private static bool TryFromAnyInterval(DateTimeInterval interval, double value, out TimeSpan timeSpan)
    {
        switch (interval)
        {
            case DateTimeInterval.Days:
                timeSpan = TimeSpan.FromDays(value);
                return true;

            case DateTimeInterval.Hours:
                timeSpan = TimeSpan.FromHours(value);
                return true;

            case DateTimeInterval.Milliseconds:
                timeSpan = TimeSpan.FromMilliseconds(value);
                return true;

            case DateTimeInterval.Minutes:
                timeSpan = TimeSpan.FromMinutes(value);
                return true;

            case DateTimeInterval.Seconds:
                timeSpan = TimeSpan.FromSeconds(value);
                return true;

            case DateTimeInterval.Ticks:
                _ = long.TryParse(value.ToString(), out long longValue);
                timeSpan = TimeSpan.FromTicks(longValue);
                return true;

            case DateTimeInterval.Weeks:
                timeSpan = TimeSpan.FromHours(24 * 7 * value);
                return true;

            default:
                timeSpan = TimeSpan.Zero;
                return false;
        }
    }
}
