namespace MGK.ServiceBase.Configuration.SeedWork
{
	/// <summary>
	/// Allows you to implement a way to have access to the configuration values without using the IConfiguration directly.
	/// </summary>
	public interface IConfigurationSetup
	{
		/// <summary>
		/// Gets the connection string for a database based on an alias.
		/// </summary>
		/// <param name="databaseAlias">The alias of the database.</param>
		/// <returns>The connection string.</returns>
		string GetConnectionString(string databaseAlias);

		/// <summary>
		/// Gets the timeout set up for the queries on a database based on an alias.
		/// </summary>
		/// <param name="databaseAlias">The alias of the database.</param>
		/// <returns>The timeout set up for the queries.</returns>
		int GetQueryTimeout(string databaseAlias);
	}
}
