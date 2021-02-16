namespace MGK.ServiceBase.DAL.SeedWork
{
	public interface IDataUnit
	{
	}

	public interface IDataUnit<TKey> : IDataUnit
	{
		TKey Id { get; set; }
	}
}
