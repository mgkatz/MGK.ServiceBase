using MediatR;

namespace MGK.ServiceBase.CQRS.Queries
{
    /// <summary>
    /// Query Base interface to abstract MediatR dependency
    /// </summary>
    /// <typeparam name="TResult">Output result</typeparam>
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
