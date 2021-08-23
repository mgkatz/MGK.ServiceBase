using FluentValidation;
using MGK.Extensions;
using MGK.ServiceBase.Constants;
using MGK.ServiceBase.Infrastructure.Exceptions;
using MGK.ServiceBase.IWEManager.Infrastructure.Extensions;
using MGK.ServiceBase.IWEManager.Infrastructure.Middlewares;
using MGK.ServiceBase.IWEManager.Models;
using MGK.ServiceBase.Services.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;

namespace MGK.ServiceBase.Infrastructure.Middlewares
{
	public class ApiExceptionHandlerMiddleware : ExceptionHandlerMiddlewareBase
	{
		public ApiExceptionHandlerMiddleware(IConfiguration configuration)
			: base(configuration)
		{
		}

		protected override string AppName => AppConfigurationKeys.AppName;

		public override ErrorDetails GetCustomErrorDetails(Exception error)
		{
			// Controlled Exceptions 
			return error switch
			{
				// Fluent Validators in IRequest Instances
				InvalidRequestException invalidRequestException => new ErrorDetails(
										StatusCodes.Status400BadRequest,
										BaseResources.MessagesResources.ErrorBadRequest.Format(invalidRequestException.Message),
										invalidRequestException.Details),

				// Any Fluent Rules Business Validations or if an input value does not
				// match the expected data type, range or pattern of the data field
				ValidationException validationException => new ErrorDetails(
										StatusCodes.Status409Conflict,
										BaseResources.MessagesResources.ErrorWithBusinessValidation,
										validationException.Message),

				// Service Validations
				ServiceValidationException serviceValidationException => new ErrorDetails(
										StatusCodes.Status409Conflict,
										ServicesResources.MessagesResources.ErrorServiceValidation,
										serviceValidationException.Message),

				// Exception Not Controlled => Internal Server Error
				_ => new ErrorDetails(
					StatusCodes.Status500InternalServerError,
					IWEResources.MessagesResources.ErrorInternalServerError,
					error.GetExceptionMesssages())
			};
		}
	}
}
