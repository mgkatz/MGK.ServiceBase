﻿using MGK.ServiceBase.DAL.Infrastructure.Enums;

namespace MGK.ServiceBase.DAL.Infrastructure.Queries;

public sealed class OrderSelector<TEntity, TKey>
    where TEntity : class, IDataUnit
{
    public Expression<Func<TEntity, TKey>> Key { get; set; }

    public SortOrder SortOrder { get; set; } = SortOrder.Ascending;
}
