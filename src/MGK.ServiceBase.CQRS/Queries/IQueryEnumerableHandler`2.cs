using MediatR;
using MGK.ServiceBase.CQRS.SeedWork;
using System.Collections.Generic;

namespace MGK.ServiceBase.CQRS.Queries
{
    /// <summary>
    /// Query Enumerable Handler Interface to abstract MediatR dependency
    /// </summary>
    /// <typeparam name="TQuery">The query to handle</typeparam>
    /// <typeparam name="TResult">Output result</typeparam>
    public interface IQueryEnumerableHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQueryEnumerable<TResult>
        where TResult : IEnumerable<IContract>
    {
    }
}
