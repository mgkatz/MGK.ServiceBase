using MediatR;
using MGK.ServiceBase.CQRS.SeedWork;
using System.Collections.Generic;

namespace MGK.ServiceBase.CQRS.Queries
{
    /// <summary>
    /// Query enumerable Base interface to abstract MediatR dependency
    /// </summary>
    /// <typeparam name="TResult">Output result</typeparam>
    public interface IQueryEnumerable<out TResult> : IRequest<TResult>
        where TResult : IEnumerable<IContract>
    {
    }
}
