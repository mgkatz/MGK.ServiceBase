namespace MGK.ServiceBase.CQRS.Commands;

/// <summary>
///  Interface to abstract IRequest MediatR
/// </summary>
public interface ICommand : IRequest
{
    Guid Id { get; }
}
