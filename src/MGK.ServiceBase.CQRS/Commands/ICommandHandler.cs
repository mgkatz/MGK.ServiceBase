namespace MGK.ServiceBase.CQRS.Commands;

/// <summary>
/// Interface to abstract IRequestHandler MediatR
/// </summary>
/// <typeparam name="TCommand">The command to handle</typeparam>
public interface ICommandHandler<in TCommand> :
    IRequestHandler<TCommand> where TCommand : ICommand
{
}
