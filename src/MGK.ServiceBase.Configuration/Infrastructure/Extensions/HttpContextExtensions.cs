using MGK.ServiceBase.Configuration.Constants;
using Microsoft.AspNetCore.Http;

namespace MGK.ServiceBase.Configuration.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions for the HttpContext.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Gets the multi-tenant alias from the HttpContext headers.
        /// </summary>
        /// <param name="source">The Http Context Accessor.</param>
        /// <returns>The multi-tenant alias requested.</returns>
        public static string GetCurrentAlias(this IHttpContextAccessor source)
            => source?.HttpContext?.Request?.Headers[HttpContextKeys.ClientAliasKey].ToString();

        /// <summary>
        /// Gets the user from the HttpContext.
        /// </summary>
        /// <param name="source">The Http Context Accessor.</param>
        /// <returns>The user name.</returns>
        public static string GetUsername(this IHttpContextAccessor source)
            => source?.HttpContext?.User?.FindFirst(HttpContextKeys.UserNameKey)?.Value;
    }
}
