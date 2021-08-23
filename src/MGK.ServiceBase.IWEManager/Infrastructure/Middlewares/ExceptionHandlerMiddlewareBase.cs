using MGK.ServiceBase.Configuration.Constants;
using MGK.ServiceBase.IWEManager.Infrastructure.Exceptions;
using MGK.ServiceBase.IWEManager.Infrastructure.Extensions;
using MGK.ServiceBase.IWEManager.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Context;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MGK.ServiceBase.IWEManager.Infrastructure.Middlewares
{
	public abstract class ExceptionHandlerMiddlewareBase
	{
        protected ExceptionHandlerMiddlewareBase(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected abstract string AppName { get; }

        protected IConfiguration Configuration { get; }

        public async virtual Task InvokeAsync(HttpContext httpContext)
		{
			var feature = httpContext.Features.Get<IExceptionHandlerPathFeature>();

			if (feature?.Error == null)
				return;

            var errorDetails = GetErrorDetails(feature);
            httpContext.Response.StatusCode = errorDetails.StatusCode;
            httpContext.Response.ContentType = ApplicationConstants.JsonContentType;
            await httpContext.Response.WriteAsync(errorDetails.ToString());

            LogContext.PushProperty("CorrelationId", httpContext.TraceIdentifier);
            Log.Error(
                "{appName} {timeStamp} {statusCode} {message} {exception}",
                Configuration[AppName],
                DateTime.UtcNow,
                errorDetails.StatusCode,
                errorDetails.Message,
                feature.Error);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        }

        /// <summary>
        /// Overridable method to extend the exceptions that can be controlled by the application.
        /// </summary>
        /// <returns>The details of the controlled error.</returns>
        public virtual ErrorDetails GetCustomErrorDetails(Exception error) => null;

        /// <summary>
        /// Evaluates if the controlled exceptions were extend or not and get the details of the error.
        /// </summary>
        /// <param name="error">The exception to be evaluated.</param>
        /// <returns>The details of the error.</returns>
        protected ErrorDetails GetOtherErrorDetails(Exception error)
        {
            var customErrorDetails = GetCustomErrorDetails(error);

            return customErrorDetails ?? new ErrorDetails(
                StatusCodes.Status500InternalServerError,
                IWEResources.MessagesResources.ErrorInternalServerError,
                error.GetExceptionMesssages());
        }

        /// <summary>
        /// Mapping Exceptions to ErrorDetails viewModel output.
        /// </summary>
        /// <param name="contextFeature">The feature that manages the exception handler.</param>
        /// <returns>The details of the errors.</returns>
        private ErrorDetails GetErrorDetails(IExceptionHandlerFeature contextFeature)
        {
            // Controlled Exceptions 
            return contextFeature.Error switch
            {
                // Parameter Validations
                ArgumentException argumentException => new ErrorDetails(
                                        StatusCodes.Status400BadRequest,
                                        IWEResources.MessagesResources.ErrorWithArgument,
                                        argumentException.Message),

                // Not Found validations
                NotFoundException notFoundException => new ErrorDetails(
                                        StatusCodes.Status404NotFound,
                                        notFoundException.Message,
                                        notFoundException.Details),

                // Exception Not Controlled => Internal Server Error
                _ => GetOtherErrorDetails(contextFeature.Error),
            };
        }
    }
}
