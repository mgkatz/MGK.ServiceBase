using MGK.ServiceBase.DAL.SeedWork;

namespace MGK.ServiceBase.DAL.Infrastructure.DataUnits
{
	public abstract class DataUnit : IDataUnit
	{
	}

	public abstract class DataUnit<TKey> : DataUnit, IDataUnit<TKey>
	{
		public TKey Id { get; set; }

		public override bool Equals(object obj)
		{
			return obj is DataUnit<TKey> dataUnit &&
				   Id.Equals(dataUnit.Id);
		}

		public override int GetHashCode() => Id.GetHashCode();
	}
}
