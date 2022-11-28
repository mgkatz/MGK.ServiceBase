namespace MGK.ServiceBase.Configuration;

/// <summary>
/// Implements a way to have at hand the information of a database server in a multi-tenant environment.
/// </summary>
public sealed class DbServerSetup : IDbServerSetup
{
	/// <summary>
	/// Gets or sets the alias that identifies the configuration of a server in a multi-tenant environment.
	/// </summary>
	public string Alias { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the information of the databases.
	/// </summary>
	public IDatabaseSetup[] Databases { get; set; } = Array.Empty<DatabaseSetup>();

	/// <summary>
	/// Gets or sets the server's name.
	/// </summary>
	public string Server { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the user's name to use to connect to the database if SQL Server Authentication is used.
	/// </summary>
	public string UserId { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the password to use to connect to the database if SQL Server Authentication is used.
	/// </summary>
	public string Password { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets if integrated security is enabled or not.
	/// </summary>
	public bool UseIntegratedSecurity { get; set; }

    /// <summary>
    /// Gets or sets if the server certificate should be trusted. This should be always false, except on local environments for testing because enabling this feature the connection process skips the trust chain validation.
    /// </summary>
    public bool TrustServerCertificate { get; set; }
}
