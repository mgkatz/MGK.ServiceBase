using FluentValidation;
using MGK.ServiceBase.API.Constants;
using MGK.ServiceBase.API.Infrastructure.Exceptions;
using MGK.ServiceBase.IWEManager.Infrastructure.Extensions;
using MGK.ServiceBase.IWEManager.Infrastructure.Middlewares;
using MGK.ServiceBase.IWEManager.Models;
using MGK.ServiceBase.Services.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MGK.ServiceBase.API.Infrastructure.Middlewares;

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
			InvalidRequestException invalidRequestException
				=> ReturnErrorDetails(
					StatusCodes.Status400BadRequest,
					BaseResources.MessagesResources.ErrorBadRequest.Format(invalidRequestException.Message),
					invalidRequestException.Details),

			// Any Fluent Rules Business Validations or if an input value does not
			// match the expected data type, range or pattern of the data field
			ValidationException validationException
				=> ReturnErrorDetails(
					StatusCodes.Status409Conflict,
					BaseResources.MessagesResources.ErrorWithBusinessValidation,
					validationException.Message),

			// Service Validations
			ServiceValidationException serviceValidationException
				=> ReturnErrorDetails(
					StatusCodes.Status409Conflict,
					ServicesResources.MessagesResources.ErrorServiceValidation,
					serviceValidationException.Message),

			// Exception Not Controlled => Internal Server Error
			_ => ReturnErrorDetails(
				StatusCodes.Status500InternalServerError,
				IWEResources.MessagesResources.ErrorInternalServerError,
				error.GetExceptionMesssages())
		};
	}

	private static ErrorDetails ReturnErrorDetails(
        int statusCode,
		string message,
		string details)
	{
		return new ErrorDetails
		{
			StatusCode = statusCode,
			Message = message,
			Details = details
		};
	}
}
