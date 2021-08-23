using MGK.Acceptance;
using MGK.Extensions;
using MGK.ServiceBase.Configuration.SeedWork;
using System;
using System.Linq;

namespace MGK.ServiceBase.Configuration.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions for the databases setup.
    /// </summary>
    public static class DatabaseSetupExtensions
    {
        /// <summary>
        /// Gets the information of a specific database from the setup through the database alias.
        /// </summary>
        /// <typeparam name="T">The type of the setup of databases.</typeparam>
        /// <param name="source">The setup of databases.</param>
        /// <param name="databaseAlias">The alias of the database.</param>
        /// <returns>The information of a specific database.</returns>
        public static IDatabaseSetup GetDatabaseItem<T>(this T source, string databaseAlias)
            where T : class, IDbServerSetup
            => source.Databases.SingleOrDefault(d => d.Alias.Equals(databaseAlias, StringComparison.InvariantCultureIgnoreCase));

        /// <summary>
        /// Validates a database from the setup through its alias.
        /// </summary>
        /// <typeparam name="T">The type of the setup of databases.</typeparam>
        /// <param name="source">The setup of databases.</param>
        /// <param name="databaseAlias">The alias of the database.</param>
        public static void ValidateDatabase<T>(this T source, string databaseAlias)
             where T : class, IDbServerSetup
        {
            Ensure.Parameter.IsNotNullNorEmptyNorWhiteSpace(databaseAlias, nameof(databaseAlias));

            if (!source.Databases.Any(d => d.Alias.Equals(databaseAlias, StringComparison.InvariantCultureIgnoreCase)))
                Raise.Error.Base(ConfigurationResources.MessagesResources.ErrorDatabaseNameNotExist.Format(databaseAlias));
        }
    }
}
