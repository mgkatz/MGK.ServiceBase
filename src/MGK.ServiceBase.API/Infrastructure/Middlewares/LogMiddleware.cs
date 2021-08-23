using MGK.Acceptance;
using MGK.ServiceBase.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MGK.ServiceBase.Infrastructure.Middlewares
{
	public class LogMiddleware
	{
		private readonly RequestDelegate _next;

		public LogMiddleware(RequestDelegate next)
		{
			if (next == null)
				Ensure.Parameter.IsNotNull(next, nameof(next));

			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			if (httpContext == null)
				Ensure.Parameter.IsNotNull(httpContext, nameof(httpContext));

			var requestStartedAt = DateTime.Now;
			var userName = string.IsNullOrWhiteSpace(httpContext?.User?.Identity?.Name)
				? "Anonymous"
				: httpContext.User.Identity.Name;

			LogContext.PushProperty("UserName", userName);
			LogContext.PushProperty("CorrelationId", httpContext.TraceIdentifier);

			// Leave the body open so the next middleware can read it.
			httpContext.Request.EnableBuffering();

			await ReadAndLogRequest(httpContext);
			await ReadAndLogResponse(httpContext);

			if (!httpContext.HasToIgnoreLogging())
				Log.Information("Request was completed in {ElapsedTime}ms", (DateTime.Now - requestStartedAt).TotalMilliseconds);
		}

		private async Task ReadAndLogRequest(HttpContext httpContext)
		{
			var requestContentLength = Convert.ToInt32(httpContext.Request.ContentLength);

			if (requestContentLength <= 0)
			{
				httpContext.LogRequestInformation();
				return;
			}

			byte[] buffer = new byte[requestContentLength];

			// Leave the body open so the next middleware can read it.
			using var reader = new StreamReader(
				httpContext.Request.Body,
				encoding: Encoding.UTF8,
				detectEncodingFromByteOrderMarks: false,
				bufferSize: buffer.Length,
				leaveOpen: true);

			var requestBody = await reader.ReadToEndAsync();

			httpContext.LogRequestInformation(requestBody);

			// Reset the request body stream position so the next middleware can read it
			httpContext.Request.Body.Position = 0;
		}

		private async Task ReadAndLogResponse(HttpContext httpContext)
		{
			// Hold a reference to the original response body stream.
			var originalResponseBodyReference = httpContext.Response.Body;

			// Target the response body to a new memory stream.
			var responseBodyMemoryStream = new MemoryStream();
			httpContext.Response.Body = responseBodyMemoryStream;

			// Handle the request.
			await HandleRequest(httpContext);

			// Read the response body.
			httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
			var responseBody = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();
			httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

			// Log information
			httpContext.LogResponseInformation(responseBody);

			// Copy the response in the memory stream to the original response stream.
			await responseBodyMemoryStream.CopyToAsync(originalResponseBodyReference);
		}

		private async Task HandleRequest(HttpContext httpContext)
			=> await _next(httpContext);
	}
}
