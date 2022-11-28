namespace MGK.ServiceBase.DAL.Infrastructure.DataUnits;

public abstract class AuditableDataUnit<TKey> : DataUnit<TKey>, IAuditableDataUnit<TKey>
{
	public DateTime CreationDate { get; set; }

	public DateTime? LastUpdateDate { get; set; }

    public override bool Equals(object obj)
        => obj is AuditableDataUnit<TKey> dataUnit &&
            base.Equals(obj) &&
            CreationDate == dataUnit.CreationDate;

    public override int GetHashCode()
		=> HashCode.Combine(base.GetHashCode(), CreationDate);
}
