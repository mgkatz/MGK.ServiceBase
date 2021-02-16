﻿using MGK.Acceptance;
using MGK.ServiceBase.CQRS.SeedWork;
using System;
using System.Runtime.Serialization;

namespace MGK.ServiceBase.CQRS.Events
{
	public abstract class EventBase : IEvent, ISerializable
	{
		public Guid Id { get; }

		public DateTime CreationDate { get; }

		public EventType EventType { get; }

		protected EventBase()
		{
			Id = Guid.NewGuid();
			CreationDate = DateTime.UtcNow;
			EventType = EventType.Domain;
		}

		protected EventBase(Guid id, DateTime creationDate, EventType eventType)
		{
			Id = id;
			CreationDate = creationDate;
			EventType = eventType;
		}

		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			Ensure.Parameter.IsNotNull(info, nameof(info));
			info.AddValue(nameof(Id), Id);
			info.AddValue(nameof(CreationDate), CreationDate);
			info.AddValue(nameof(EventType), EventType);
		}

		public override bool Equals(object obj)
		{
			return obj is EventBase eventBase &&
				   Id.Equals(eventBase.Id) &&
				   CreationDate == eventBase.CreationDate &&
				   EventType == eventBase.EventType;
		}

		public override int GetHashCode()
		{
			var hashCode = Id.GetHashCode();
			hashCode = (hashCode * 397) ^ CreationDate.GetHashCode();
			hashCode = (hashCode * 397) ^ EventType.GetHashCode();
			return hashCode;
		}
	}
}
