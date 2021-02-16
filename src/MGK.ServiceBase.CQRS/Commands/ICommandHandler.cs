using MediatR;
using MGK.ServiceBase.CQRS.SeedWork;

namespace MGK.ServiceBase.CQRS.Commands
{
    /// <summary>
    /// Interface to abstract IRequestHandler MediatR
    /// </summary>
    /// <typeparam name="TCommand">The command to handle</typeparam>
    public interface ICommandHandler<in TCommand> :
        IRequestHandler<TCommand> where TCommand : ICommand
    {
    }

    /// <summary>
    /// Interface to abstract IRequestHandler MediatR
    /// </summary>
    /// <typeparam name="TCommand">The command to handle</typeparam>
    /// <typeparam name="TResult">Output result</typeparam>
    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
        where TResult : IContract
    {
    }
}
