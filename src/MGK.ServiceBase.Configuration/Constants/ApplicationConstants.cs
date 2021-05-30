namespace MGK.ServiceBase.Configuration.Constants
{
	/// <summary>
	/// Represents a shortcut to keys used by the application globally.
	/// </summary>
	public struct ApplicationConstants
	{
		/// <summary>
		/// The prefix of the bearer token.
		/// </summary>
		public const string BearerToken = "Bearer";

		/// <summary>
		/// The name of the environment variable used to build the application configuration.
		/// </summary>
		public const string EnvironmentName = "ASPNETCORE_ENVIRONMENT";

		/// <summary>
		/// The content type by default for Json.
		/// </summary>
		public const string JsonContentType = "application/json";
	}
}
