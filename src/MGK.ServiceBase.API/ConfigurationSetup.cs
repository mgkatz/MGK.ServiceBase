using MGK.Acceptance;
using MGK.Extensions;
using MGK.ServiceBase.Infrastructure.Extensions;
using MGK.ServiceBase.SeedWork;
using Microsoft.Extensions.Configuration;
using MGK.ServiceBase.Constants;

namespace MGK.ServiceBase
{
    /// <summary>
    /// Implement a way to have access to the configuration values without using the IConfiguration directly.
    /// </summary>
    public sealed class ConfigurationSetup : IConfigurationSetup
    {
        private readonly IDbServerSetup _databaseSetup;

		/// <summary>
		/// Creates an instance of the configuration setup.
		/// </summary>
		/// <param name="configuration">The configuration settings.</param>
		/// <param name="multiTenantSetup">The multi tenant information.</param>
		public ConfigurationSetup(
            IConfiguration configuration
            , IMultiTenantSetup multiTenantSetup
            , IServiceParameters serviceParameters)
        {
            Ensure.Parameter.IsNotNull(configuration, nameof(configuration));

            if (configuration.GetValue<bool>(AppConfigurationKeys.UseMultiTenant))
            {
                Ensure.Parameter.IsNotNull(multiTenantSetup, nameof(multiTenantSetup));
                _databaseSetup = GetDatabaseSetup(configuration, multiTenantSetup.CurrentClientAlias);
            }
            else
            {
                Ensure.Parameter.IsNotNull(serviceParameters, nameof(serviceParameters));
                _databaseSetup = GetDatabaseSetup(configuration, serviceParameters.ClientAlias);
			}
		}

        /// <summary>
        /// Gets the connection string for a database based on an alias.
        /// </summary>
        /// <param name="databaseAlias">The alias of the database.</param>
        /// <returns>The connection string.</returns>
        public string GetConnectionString(string databaseAlias)
        {
            _databaseSetup.ValidateDatabase(databaseAlias);
            var dbItem = _databaseSetup.GetDatabaseItem(databaseAlias);

            return _databaseSetup.UseIntegratedSecurity
                ? BaseResources.ContextResources.ConnectionStringIntegratedSecurity
                    .Format(_databaseSetup.Server, dbItem.Name, "True")
                : BaseResources.ContextResources.ConnectionStringUserPassword
                    .Format(_databaseSetup.Server, dbItem.Name, _databaseSetup.UserId, _databaseSetup.Password);
        }

        /// <summary>
        /// Gets the timeout set up for the queries on a database based on an alias.
        /// </summary>
        /// <param name="databaseAlias">The alias of the database.</param>
        /// <returns>The timeout set up for the queries.</returns>
        public int GetQueryTimeout(string databaseAlias)
        {
            _databaseSetup.ValidateDatabase(databaseAlias);
            var dbItem = _databaseSetup.GetDatabaseItem(databaseAlias);
            return dbItem.QueryTimeout;
        }

        private IDbServerSetup GetDatabaseSetup(IConfiguration configuration, string clientAlias)
        {
            IDbServerSetup databaseSetup = configuration.GetDatabaseSetup(clientAlias);

            Ensure.Value.IsNotNull(
                databaseSetup,
                BaseResources.MessagesResources
                    .ErrorDatabaseConfigurationNotAvailable
                    .Format(clientAlias ?? string.Empty));

            return databaseSetup;
        }
    }
}
