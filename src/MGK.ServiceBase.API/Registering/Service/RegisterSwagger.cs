using MGK.ServiceBase.Configuration.Constants;
using MGK.ServiceBase.Configuration.SeedWork;
using MGK.ServiceBase.Constants;
using MGK.ServiceBase.Infrastructure.Filters;
using MGK.ServiceBase.Services.Infrastructure.Exceptions;
using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace MGK.ServiceBase.Registering.Service
{
    public class RegisterSwagger : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            var serviceParameters = serviceProvider.GetService<IMicroServiceParameters>();

            if (serviceParameters == null)
                throw new ServiceValidationException(BaseResources.MessagesResources.ErrorSwaggerRegistrationTitle, BaseResources.MessagesResources.ErrorSwaggerRegistrationDescription);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(configuration[AppConfigurationKeys.ApiVersion], GetOpenApiInfo(configuration));
                options.AddSecurityDefinition(ApplicationConstants.BearerToken, GetOpenApiSecurityScheme());
                options.CustomSchemaIds(x => x.FullName);
                options.OperationFilter<SwaggerAuthorizeCheckOperationFilter>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{serviceParameters.SwaggerDocumentName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            serviceProvider.Dispose();
        }

        private static OpenApiInfo GetOpenApiInfo(IConfiguration configuration)
            => new()
			{
                Title = configuration[AppConfigurationKeys.AppName],
                Version = configuration[AppConfigurationKeys.ApiVersion]
            };

        private static OpenApiSecurityScheme GetOpenApiSecurityScheme()
            => new()
			{
                Scheme = ApplicationConstants.BearerToken,
                Description = BaseResources.MessagesResources.InformationSwaggerDescription,
                Name = BaseResources.MessagesResources.InformationSwaggerName,
                Type = SecuritySchemeType.Http,
            };
    }
}
