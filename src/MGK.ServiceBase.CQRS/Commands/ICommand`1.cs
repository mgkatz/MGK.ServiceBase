namespace MGK.ServiceBase.CQRS.Commands;

/// <summary>
/// Interface to abstract IRequest MediatR
/// </summary>
/// <typeparam name="TResult">Output result</typeparam>
public interface ICommand<out TResult> : IRequest<TResult>
    where TResult : IContract
{
    Guid Id { get; }
}
