namespace MGK.ServiceBase.Configuration.Constants
{
	/// <summary>
	/// Represents a shortcut to keys in the Http context headers.
	/// </summary>
	public struct HttpContextKeys
	{
		/// <summary>
		/// The key in the Http context header for a multi-tenant alias.
		/// </summary>
		public const string ClientAliasKey = "clientalias";

		/// <summary>
		/// The key int the Http context header for the user name.
		/// </summary>
		public const string UserNameKey = "username";
	}
}
