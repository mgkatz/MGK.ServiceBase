using MGK.ServiceBase.CQRS.Events;
using System;
using System.Runtime.Serialization;

namespace MGK.ServiceBase.CQRS.SeedWork
{
	public interface IEvent
	{
		Guid Id { get; }
		DateTime CreationDate { get; }
		EventType EventType { get; }
		void GetObjectData(SerializationInfo info, StreamingContext context);
	}
}
