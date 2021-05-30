namespace MGK.ServiceBase.Configuration.SeedWork
{
	/// <summary>
	/// Allows you to implement a way to have at hand the information of a database server in a multi-tenant environment.
	/// </summary>
	public interface IDbServerSetup
	{
		/// <summary>
		/// Gets the alias that identifies the configuration of a server in a multi-tenant environment.
		/// </summary>
		string Alias { get; }

		/// <summary>
		/// Gets the information of the databases.
		/// </summary>
		IDatabaseSetup[] Databases { get; }

		/// <summary>
		/// Gets the server's name.
		/// </summary>
		string Server { get; }

		/// <summary>
		/// Gets the user's name to use to connect to the database if SQL Server Authentication is used.
		/// </summary>
		string UserId { get; }

		/// <summary>
		/// Gets the password to use to connect to the database if SQL Server Authentication is used.
		/// </summary>
		string Password { get; }

		/// <summary>
		/// Gets if integrated security is enabled or not.
		/// </summary>
		bool UseIntegratedSecurity { get; }
	}
}
