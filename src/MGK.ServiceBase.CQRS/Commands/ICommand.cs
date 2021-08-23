using MediatR;
using MGK.ServiceBase.CQRS.SeedWork;
using System;

namespace MGK.ServiceBase.CQRS.Commands
{
    /// <summary>
    ///  Interface to abstract IRequest MediatR
    /// </summary>
    public interface ICommand : IRequest
    {
        Guid Id { get; }
    }

    /// <summary>
    /// Interface to abstract IRequest MediatR
    /// </summary>
    /// <typeparam name="TResult">Output result</typeparam>
    public interface ICommand<out TResult> : IRequest<TResult>
        where TResult : IContract
    {
        Guid Id { get; }
    }
}
