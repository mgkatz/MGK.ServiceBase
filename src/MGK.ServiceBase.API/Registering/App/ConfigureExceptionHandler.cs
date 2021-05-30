using MGK.ServiceBase.Infrastructure.Middlewares;
using MGK.ServiceBase.SeedWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace MGK.ServiceBase.Registering.App
{
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
}
