using MGK.Extensions.Enums;

namespace MGK.ServiceBase.Services.SeedWork;

public interface ICacheManager : IService
{
    void AddToCache<T>(
        object key,
        T valueToCache,
        DateTimeInterval interval = DateTimeInterval.Minutes,
        double intervalValue = 30)
        where T : class;

    void AddToCache<T>(
        object key,
        IEnumerable<T> valueToCache,
        DateTimeInterval interval = DateTimeInterval.Minutes,
        double intervalValue = 30)
        where T : class;

    bool TryGetValue(object key, out object value);
}
