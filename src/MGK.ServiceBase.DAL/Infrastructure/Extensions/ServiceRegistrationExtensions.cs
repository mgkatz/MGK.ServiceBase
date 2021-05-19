using MGK.Extensions;
using MGK.ServiceBase.Configuration.SeedWork;
using MGK.ServiceBase.DAL.Infrastructure.Factories;
using MGK.ServiceBase.Services.Infrastructure.Extensions;
using MGK.ServiceBase.Services.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MGK.ServiceBase.DAL.Infrastructure.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServicesInAssembly<SqlConnectionFactory>(configuration);
        }

        public static void AddDbContext<T>(this IServiceCollection services,
            string databaseKey,
            int maxRetryCount = 10,
            int maxRetryDelay = 30,
            ICollection<int> errorNumbersToAdd = null) where T : DbContext
        {
            // The instance of the DbContext object has to have its service lifetime set to ServiceLifetime.Scoped,
            // which is the default lifetime when registering a DbContext with services.AddDbContext
            services.AddDbContext<T>(options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var configurationSetup = serviceProvider.GetService<IConfigurationSetup>();
                var connectionString = configurationSetup.GetConnectionString(databaseKey);

                if (connectionString.IsNullOrEmptyOrWhiteSpace())
                    throw new ServiceValidationException(DALResources.MessagesResources.ErrorConnectionStringName, DALResources.MessagesResources.ErrorConnectionStringIsEmpty);

                options.UseSqlServer(connectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(T).Assembly.GetName().Name);
                        // Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                        sqlOptions.EnableRetryOnFailure(maxRetryCount, TimeSpan.FromSeconds(maxRetryDelay), errorNumbersToAdd);
                    });

                serviceProvider.Dispose();
            });

            services.AddScoped<T>();
        }
    }
}
