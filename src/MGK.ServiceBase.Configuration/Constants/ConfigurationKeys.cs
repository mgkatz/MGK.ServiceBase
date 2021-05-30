namespace MGK.ServiceBase.Configuration.Constants
{
	/// <summary>
	/// Represents a shortcut to keys in the application configuration files.
	/// </summary>
	public struct ConfigurationKeys
	{
		/// <summary>
		/// The path in the application configuration file for the name of the vault in Azure.
		/// </summary>
		public const string AzVaultName = "keyVault:vault";

		/// <summary>
		/// The path in the application configuration file for the public key of the application allowed in an Azure Key Vault.
		/// </summary>
		public const string AzVaultClientId = "keyVault:clientId";

		/// <summary>
		/// The path in the application configuration file for the public key of the secret in an Azure Key Vault.
		/// </summary>
		public const string AzVaultClientSecret = "keyVault:clientSecret";

		/// <summary>
		/// The path in the application configuration file for the name of the database secret in an Azure Key Vault.
		/// </summary>
		public const string AzVaultSecretName = "keyVault:dbSecretName";

		/// <summary>
		/// The path in the application configuration file for the setup information of the database.
		/// </summary>
		public const string DatabaseSetup = "databaseSetup";

		/// <summary>
		/// The path in a non-productive application configuration file for a multi-tenant alias to use for testing.
		/// </summary>
		public const string DebugClientAlias = "debugenv:clientalias";

		/// <summary>
		/// The path in a non-productive application configuration file to evaluate if multi-tenant will ben enabled.
		/// </summary>
		public const string UseMultiTenant = "useMultiTenant";
	}
}
