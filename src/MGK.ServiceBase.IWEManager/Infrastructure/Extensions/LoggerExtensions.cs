using Microsoft.Extensions.Logging;

namespace MGK.ServiceBase.IWEManager.Infrastructure.Extensions;

public static class LoggerExtensions
{
    public static void EnsureIsAlive(this ILogger source)
    {
        Ensure.Parameter.IsNotNull(source, nameof(source), IWEResources.MessagesResources.ErrorLogger);
    }

    public static void EnsureIsAlive<T>(this ILogger<T> source)
        where T : class
    {
        Ensure.Parameter.IsNotNull(source, nameof(source), IWEResources.MessagesResources.ErrorLogger);
    }
}
