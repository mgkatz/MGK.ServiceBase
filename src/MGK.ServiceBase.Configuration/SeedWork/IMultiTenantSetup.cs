using System.Collections.Generic;

namespace MGK.ServiceBase.Configuration.SeedWork
{
	/// <summary>
	/// Allows you to implement a way to have at hand information of a multi-tenant environment.
	/// </summary>
	public interface IMultiTenantSetup
	{
		/// <summary>
		/// Gets the client alias that is currently configured.
		/// </summary>
		string CurrentClientAlias { get; }

		/// <summary>
		/// Gets the information of the databases configured by alias.
		/// </summary>
		IDictionary<string, IDbServerSetup> DatabasesByAliases { get; }
	}
}
