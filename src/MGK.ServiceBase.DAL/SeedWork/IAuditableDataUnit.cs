using System;

namespace MGK.ServiceBase.DAL.SeedWork
{
	public interface IAuditableDataUnit : IDataUnit
	{
		DateTime CreationDate { get; set; }

		DateTime? LastUpdateDate { get; set; }
	}

	public interface IAuditableDataUnit<TKey> : IAuditableDataUnit, IDataUnit<TKey>
	{
	}
}
