using MGK.ServiceBase.API.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace MGK.ServiceBase.API.Registering.App;

	public class ConfigureExceptionHandler : IAppBuilderConfiguration
{
    public virtual void ConfigureApp(IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseExceptionHandler(new ExceptionHandlerOptions
        {
            AllowStatusCode404Response = true,
            ExceptionHandler = new ApiExceptionHandlerMiddleware(configuration).InvokeAsync
        });
    }
}
