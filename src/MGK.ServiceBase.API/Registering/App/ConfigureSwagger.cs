using MGK.Extensions;
using MGK.ServiceBase.Constants;
using MGK.ServiceBase.Infrastructure.Exceptions;
using MGK.ServiceBase.SeedWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.Registering.App
{
    public class ConfigureSwagger : IAppBuilderConfiguration
    {
        public virtual void ConfigureApp(IApplicationBuilder app, IConfiguration configuration)
        {
            var serviceParameters = app.ApplicationServices.GetService<IServiceParameters>();

            if (serviceParameters.ContextPath.IsNullOrEmptyOrWhiteSpace())
            {
                throw new ApplicationValidationException(BaseResources.MessagesResources.ErrorInternalServerError, BaseResources.MessagesResources.ErrorContextPathNotDefined);
            }

            const string swagger = "swagger";
            var swaggerFile = $"{swagger}.json";
            var routePrefix = serviceParameters.ContextPath + swagger;

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c => c.RouteTemplate = $"{routePrefix}/{{documentName}}/{swaggerFile}");

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    $"{configuration[AppConfigurationKeys.ApiVersion]}/{swaggerFile}",
                    configuration[AppConfigurationKeys.AppName]);
                c.RoutePrefix = routePrefix;
            });
        }
    }
}
