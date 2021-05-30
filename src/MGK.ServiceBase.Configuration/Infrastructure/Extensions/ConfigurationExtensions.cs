using MGK.Acceptance;
using MGK.ServiceBase.Configuration.Constants;
using MGK.ServiceBase.Configuration.SeedWork;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MGK.ServiceBase.Configuration.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions for the application configuration.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Gets the database server information through an alias from the application configuration.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="clientAlias">The alias that identifies a database server.</param>
        /// <returns>The configuration of the database server.</returns>
        public static IDbServerSetup GetDatabaseSetup(this IConfiguration configuration, string clientAlias)
        {
            Ensure.Parameter.IsNotNullNorEmptyNorWhiteSpace(
                clientAlias,
                nameof(clientAlias),
                ConfigurationResources.MessagesResources.ErrorAliasNotExist);

            var databasesList = configuration.GetDatabaseSetupList();

            return databasesList[clientAlias];
        }

        /// <summary>
        /// Gets the list of database servers from the application configuration using Azure Key Vault.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <returns>The list of database servers.</returns>
        public static IDictionary<string, IDbServerSetup> GetDatabaseSetupList(this IConfiguration configuration)
        {
            var databasesList = Array.Empty<DbServerSetup>();

            if (!configuration.GetValue<bool>(ConfigurationKeys.UseMultiTenant))
            {
                var dbsInConfig = new List<DbServerSetup>();
                configuration.GetSection(ConfigurationKeys.DatabaseSetup).Bind(dbsInConfig);
                databasesList = dbsInConfig.ToArray();
            }
            else
            {
                var secretName = configuration.GetSection(ConfigurationKeys.AzVaultSecretName).Value;
                var databaseSetupValue = configuration.GetSection(secretName).Value;
                Ensure.Value.IsNotNull(databaseSetupValue);
                databasesList = JsonConvert.DeserializeObject<DbServerSetup[]>(databaseSetupValue);
            }

            return databasesList.ToDictionary(k => k.Alias, v => v as IDbServerSetup);
        }
    }
}
