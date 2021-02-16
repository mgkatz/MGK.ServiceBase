using FluentValidation;
using MGK.Extensions;
using MGK.ServiceBase.Constants;
using MGK.ServiceBase.Infrastructure.Exceptions;
using MGK.ServiceBase.Infrastructure.Extensions;
using MGK.ServiceBase.SeedWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace MGK.ServiceBase.Registering.App
{
	public class ConfigureExceptionHandler : IAppBuilderConfiguration
    {
        public virtual void ConfigureApp(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = ApplicationConstants.JsonContentType;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        var errorDetails = GetErrorDetails(contextFeature);
                        context.Response.StatusCode = errorDetails.StatusCode;

                        Log.Error(
                            "{appName} {timeStamp} {statusCode} {message} {exception}",
                            configuration[AppConfigurationKeys.AppName],
                            DateTime.UtcNow,
                            errorDetails.StatusCode,
                            errorDetails.Message,
                            contextFeature.Error);

                        await context.Response.WriteAsync(errorDetails.ToString());
                    }
                });
            });
        }

        /// <summary>
        /// Overridable method to extend the exceptions that can be controlled by the application.
        /// </summary>
        /// <returns>The details of the controlled error.</returns>
        protected virtual ErrorDetails GetCustomErrorDetails(Exception error) => null;

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
				// Fluent Validators in IRequest Instances
				InvalidRequestException invalidRequestException => new ErrorDetails(
										StatusCodes.Status400BadRequest,
										BaseResources.MessagesResources.ErrorBadRequest.Format(invalidRequestException.Message),
										invalidRequestException.Details),

				// Not Found validations
				NotFoundException notFoundException => new ErrorDetails(
										StatusCodes.Status404NotFound,
										notFoundException.Message,
										notFoundException.Details),

				// Parameter Validations
				ArgumentException argumentException => new ErrorDetails(
										StatusCodes.Status409Conflict,
										BaseResources.MessagesResources.ErrorWithArgument,
										argumentException.Message),

				// Any Fluent Rules Business Validations or if an input value does not
				// match the expected data type, range or pattern of the data field
				ValidationException validationException => new ErrorDetails(
										StatusCodes.Status409Conflict,
										BaseResources.MessagesResources.ErrorWithBusinessValidation,
										validationException.Message),

				// Service Validations
				ServiceValidationException serviceValidationException => new ErrorDetails(
										StatusCodes.Status409Conflict,
										BaseResources.MessagesResources.ErrorServiceValidation,
										serviceValidationException.Message),

				// Exception Not Controlled => Internal Server Error
				_ => GetOtherErrorDetails(contextFeature.Error),
			};
		}

        /// <summary>
        /// Evaluates if the controlled exceptions were extend or not and get the details of the error.
        /// </summary>
        /// <param name="error">The exception to be evaluated.</param>
        /// <returns>The details of the error.</returns>
        private ErrorDetails GetOtherErrorDetails(Exception error)
        {
            var customErrorDetails = GetCustomErrorDetails(error);

            return customErrorDetails ?? new ErrorDetails(
				StatusCodes.Status500InternalServerError,
				BaseResources.MessagesResources.ErrorInternalServerError,
				error.GetExceptionMesssages());
        }
    }
}
