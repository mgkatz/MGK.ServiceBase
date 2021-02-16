using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace MGK.ServiceBase.Infrastructure.Exceptions
{
    [Serializable]
	[SuppressMessage("Design", "RCS1194:Implement exception constructors.", Justification = "<Pending>")]
	public abstract class BaseException : Exception
    {
        protected BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        protected BaseException(string message, string details)
            : base(message)
        {
            Details = details;
        }

        public string Details { get; }
    }
}
