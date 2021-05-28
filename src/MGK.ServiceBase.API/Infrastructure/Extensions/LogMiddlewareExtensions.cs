using MGK.ServiceBase.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace MGK.ServiceBase.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions for log's middlewares.
    /// </summary>
    public static class LogMiddlewareExtensions
    {
        /// <summary>
        /// Enables the Log Middleware through the application builder.
        /// </summary>
        /// <param name="source">The application builder.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder source)
            => source.UseMiddleware<LogMiddleware>();
    }
}
