using MediatR;
using MGK.ServiceBase.CQRS.SeedWork;

namespace MGK.ServiceBase.CQRS.Queries
{
    /// <summary>
    /// Query Handler Interface to abstract MediatR dependency
    /// </summary>
    /// <typeparam name="TQuery">The query to handle</typeparam>
    /// <typeparam name="TResult">Output result</typeparam>
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
        where TResult : IContract
    {
    }
}
