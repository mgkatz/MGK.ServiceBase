namespace MGK.ServiceBase.Configuration.SeedWork
{
	/// <summary>
	/// Allows you to implement a way to have at hand the information of a database.
	/// </summary>
	public interface IDatabaseSetup
	{
		/// <summary>
		/// Gets the alias of the database.
		/// </summary>
		string Alias { get; }

		/// <summary>
		/// Gets the name of the database.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the timeout set up for the queries on a database.
		/// </summary>
		int QueryTimeout { get; }
	}
}
