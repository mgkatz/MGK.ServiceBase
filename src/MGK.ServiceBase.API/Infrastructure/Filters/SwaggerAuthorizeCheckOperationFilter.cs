using MGK.ServiceBase.Configuration.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MGK.ServiceBase.Infrastructure.Filters
{
    public class SwaggerAuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Check for authorize attribute
            var hasAuthorize = context.MethodInfo.DeclaringType != null &&
                               (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
                                || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any());

            if (!hasAuthorize) return;

            operation.Responses.TryAdd(Convert.ToString((int)HttpStatusCode.Unauthorized),
                new OpenApiResponse { Description = nameof(HttpStatusCode.Unauthorized) });

            operation.Responses.TryAdd(Convert.ToString((int)HttpStatusCode.Forbidden),
                new OpenApiResponse { Description = nameof(HttpStatusCode.Forbidden) });

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = ApplicationConstants.BearerToken,
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                }
            };
        }
    }
}
