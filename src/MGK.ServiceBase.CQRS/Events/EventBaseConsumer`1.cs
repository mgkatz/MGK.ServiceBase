using SlimMessageBus;
using System.Threading.Tasks;

namespace MGK.ServiceBase.CQRS.Events
{
	public abstract class EventBaseConsumer<T> : IConsumer<T> where T : EventBase
	{
		protected EventBaseConsumer()
		{
		}

		public abstract Task OnHandle(T message, string name);
	}
}
