using MGK.ServiceBase.IWEManager.Infrastructure.Exceptions;
using System.Runtime.Serialization;

namespace MGK.ServiceBase.Security.Infrastructure.Exceptions;

public class TokenException : BaseException
{
    protected TokenException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Exception for issues when a token has expired.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    public TokenException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Exception for issues when a token has expired.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    /// <param name="details">Details to show in response</param>
    public TokenException(string message, string details)
        : base(message, details)
    {
    }

    /// <summary>
    /// Exception for issues when a token has expired.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    /// <param name="details">Details to show in response</param>
    /// <param name="innerException">Inner exception for the response</param>
    public TokenException(string message, string details, Exception innerException)
        : base(message, details, innerException)
    {
    }
}
