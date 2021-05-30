using MGK.ServiceBase.CQRS.SeedWork;

namespace MGK.ServiceBase.CQRS.Queries
{
    public abstract class QueryBase<TResult> : IQuery<TResult>
        where TResult : class, IContract
    {
        protected QueryBase()
        {
        }
    }
}
