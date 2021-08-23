using MGK.ServiceBase.CQRS.SeedWork;
using System;

namespace MGK.ServiceBase.CQRS.Commands
{
    /// <summary>
    /// CommandBase Class to create Guid command identifier
    /// </summary>
    /// <typeparam name="TResult">Output result</typeparam>
    public abstract class CommandBase<TResult> : ICommand<TResult>
        where TResult : IContract
    {
        protected CommandBase()
        {
            Id = Guid.NewGuid();
        }

        protected CommandBase(Guid commandId)
        {
            Id = commandId;
        }

        public Guid Id { get; }

        public override bool Equals(object obj)
        {
            return obj is CommandBase<TResult> commandBase &&
                   Id.Equals(commandBase.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
