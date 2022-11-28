using MGK.ServiceBase.IWEManager.Infrastructure.Exceptions;
using System.Runtime.Serialization;

namespace MGK.ServiceBase.API.Infrastructure.Exceptions;

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
    public InvalidRequestException(string message)
        : base(message)
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

    /// <summary>
    /// Exception for request not valid.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    /// <param name="details">Details to show in response</param>
    /// <param name="innerException">Inner exception for the response</param>
    public InvalidRequestException(string message, string details, Exception innerException)
        : base(message, details, innerException)
    {
    }
}
