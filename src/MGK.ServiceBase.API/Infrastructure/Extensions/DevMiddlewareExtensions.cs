using MGK.ServiceBase.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace MGK.ServiceBase.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions for developer's middlewares.
    /// </summary>
    public static class DevMiddlewareExtensions
    {
        /// <summary>
        /// Enables the Dev Middleware through the application builder.
        /// </summary>
        /// <param name="source">The application builder.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseDevMiddleware(this IApplicationBuilder source)
            => source.UseMiddleware<DevMiddleware>();
    }
}
