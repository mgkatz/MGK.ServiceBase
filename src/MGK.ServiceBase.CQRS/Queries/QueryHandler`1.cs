using MGK.ServiceBase.CQRS.SeedWork;
using MGK.ServiceBase.Services;

namespace MGK.ServiceBase.CQRS.Queries
{
    public abstract class QueryHandler<T> : ServiceHandler<T>
        where T : class, IHandlerService
    {
        protected QueryHandler(ICqrsInternalServices<T> internalServices)
            : base(internalServices)
        {
        }
    }
}
