using MGK.ServiceBase.Configuration.SeedWork;

namespace MGK.ServiceBase.Configuration
{
	/// <summary>
	/// Implements a way to have at hand the information of a database.
	/// </summary>
	public sealed class DatabaseSetup : IDatabaseSetup
	{
		/// <summary>
		/// Gets or sets the alias of the database.
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		/// Gets or sets the name of the database.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the timeout set up for the queries on a database.
		/// </summary>
		public int QueryTimeout { get; set; }
	}
}
