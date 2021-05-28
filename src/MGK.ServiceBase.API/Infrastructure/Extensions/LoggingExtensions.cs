using MGK.Extensions;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;
using System.Linq;

namespace MGK.ServiceBase.Infrastructure.Extensions
{
	public static class LoggingExtensions
	{
		private static readonly string[] _baseLogExclusions = new string[] { "swagger", "favicon" };

		public static bool HasToIgnoreLogging(this HttpContext httpContext, params string[] logExclusions)
		{
			var newLogExclussions = logExclusions?.Length > 0
				? _baseLogExclusions
				: _baseLogExclusions.Union(logExclusions).ToArray();

			return httpContext.Request.Path.HasValue && httpContext.Request.Path.Value.In(newLogExclussions, true);
		}

		public static void LogRequestInformation(this HttpContext httpContext, string requestBody = null)
		{
			if (httpContext.HasToIgnoreLogging())
				return;

			LogContext.PushProperty(
				"RequestHeaders",
				httpContext.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
				destructureObjects: true);

			Log.Information(BaseResources.MessagesResources.InfoRequestStart);
			Log.Information(BaseResources.MessagesResources.InfoRequestHeaders);
			Log.Information(
				BaseResources.MessagesResources.InfoRequestMethodPath,
				httpContext.Request.Method,
				httpContext.Request.Path);

			if (httpContext.Request.QueryString.HasValue)
				Log.Information(BaseResources.MessagesResources.InfoRequestQueryString, httpContext.Request.QueryString.Value);

			if (!requestBody.IsNullOrEmptyOrWhiteSpace())
				Log.Information(BaseResources.MessagesResources.InfoRequestBody, requestBody);
		}

		public static void LogResponseInformation(this HttpContext httpContext, string responseBody)
		{
			if (httpContext.HasToIgnoreLogging())
				return;

			Log.Information(
				BaseResources.MessagesResources.InfoResponseGeneral,
				httpContext.Request.Method,
				httpContext.Request.Path,
				httpContext.Response.StatusCode);
			Log.Information(BaseResources.MessagesResources.InfoResponseBody, responseBody);
			Log.Information(BaseResources.MessagesResources.InfoResponseContentType, httpContext.Response.ContentType);
		}
	}
}
