using MGK.ServiceBase.IWEManager.Infrastructure.Exceptions;
using System;
using System.Runtime.Serialization;

namespace MGK.ServiceBase.Infrastructure.Exceptions
{
    [Serializable]
    public class InvalidRequestException : BaseException
    {
        protected InvalidRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Exception for request not valid.
        /// </summary>
        /// <param name="message">Message to show in response</param>
        /// <param name="details">Details to show in response</param>
        public InvalidRequestException(string message, string details)
            : base(message, details)
        {
        }
    }
}
