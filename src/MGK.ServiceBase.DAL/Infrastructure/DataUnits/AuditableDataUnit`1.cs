using MGK.ServiceBase.DAL.SeedWork;
using System;

namespace MGK.ServiceBase.DAL.Infrastructure.DataUnits
{
	public abstract class AuditableDataUnit<TKey> : DataUnit<TKey>, IAuditableDataUnit<TKey>
	{
		public DateTime CreationDate { get; set; }

		public DateTime? LastUpdateDate { get; set; }

		public override bool Equals(object obj)
		{
			return obj is AuditableDataUnit<TKey> dataUnit &&
				   base.Equals(obj) &&
				   CreationDate == dataUnit.CreationDate;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(base.GetHashCode(), CreationDate);
		}
	}
}
