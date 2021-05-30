using MGK.ServiceBase.CQRS.SeedWork;
using MGK.ServiceBase.Services;

namespace MGK.ServiceBase.CQRS.Commands
{
    public abstract class CommandHandler<T> : ServiceBase<T>
        where T : class, IHandlerService
    {
        protected CommandHandler(ICqrsInternalServices<T> internalServices)
            : base(internalServices)
        {
        }
    }
}
