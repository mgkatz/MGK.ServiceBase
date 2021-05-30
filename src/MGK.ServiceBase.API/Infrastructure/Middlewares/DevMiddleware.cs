using MGK.Acceptance;
using MGK.Extensions;
using MGK.ServiceBase.Configuration.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace MGK.ServiceBase.Infrastructure.Middlewares
{
	/// <summary>
	/// Implements a middleware for developers that allows to take values from the application configuration files and set them in the http context.
	/// </summary>
	public class DevMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Creates an instance of the middleware for developers.
		/// </summary>
		/// <param name="next">The request.</param>
		/// <param name="configuration">The application configuration.</param>
		public DevMiddleware(RequestDelegate next, IConfiguration configuration)
		{
			_next = next;
			_configuration = configuration;
		}

		/// <summary>
		/// Captures the http context and allows to do actions before continue registering the application.
		/// </summary>
		/// <param name="context">The http context.</param>
		public async Task InvokeAsync(HttpContext context)
		{
			Ensure.Parameter.IsNotNull(context, nameof(context));
			ConfigureHttpContext(context);
			await _next(context);
		}

		/// <summary>
		/// Overridable method that allows to configure the http context.
		/// </summary>
		/// <param name="context">The http context.</param>
		public virtual void ConfigureHttpContext(HttpContext context)
		{
			var debugClientAlias = _configuration.GetValue<string>(ConfigurationKeys.DebugClientAlias);

			if (!debugClientAlias.IsNullOrEmptyOrWhiteSpace())
			{
				context
					.Request
					.Headers
					.Add(HttpContextKeys.ClientAliasKey, new StringValues(debugClientAlias));
			}
		}
	}
}
