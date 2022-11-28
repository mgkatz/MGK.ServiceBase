using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MGK.ServiceBase.Configuration.Infrastructure.Extensions;

public static class OptionsExtensions
{
    public static void EnsureIsAlive<T>(
        this IOptions<T> source,
        ILogger logger)
        where T : class
    {
        var errorMessage = ConfigurationResources.MessagesResources.ErrorOptions.Format(typeof(T).Name);

        if (source is null && logger is not null)
        {
            logger.LogError(message: errorMessage);
        }

        Ensure.Parameter.IsNotNull(source, nameof(source), errorMessage);
    }
}
