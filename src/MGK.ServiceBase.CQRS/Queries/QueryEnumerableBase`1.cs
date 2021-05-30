using MGK.ServiceBase.CQRS.SeedWork;
using System.Collections.Generic;

namespace MGK.ServiceBase.CQRS.Queries
{
    public abstract class QueryEnumerableBase<TResult> : IQueryEnumerable<TResult>
        where TResult : class, IEnumerable<IContract>
    {
        protected QueryEnumerableBase()
        {
        }
    }
}
