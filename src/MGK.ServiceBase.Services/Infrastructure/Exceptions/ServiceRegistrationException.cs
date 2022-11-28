using MGK.ServiceBase.IWEManager.Infrastructure.Exceptions;
using System.Runtime.Serialization;

namespace MGK.ServiceBase.Services.Infrastructure.Exceptions;

[Serializable]
public class ServiceRegistrationException : BaseException
{
    protected ServiceRegistrationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Exception for registration exceptions on services.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    public ServiceRegistrationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Exception for registration exceptions on services.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    /// <param name="details">Details to show in response</param>
    public ServiceRegistrationException(string message, string details)
        : base(message, details)
    {
    }

    /// <summary>
    /// Exception for registration exceptions on services.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    /// <param name="details">Details to show in response</param>
    /// <param name="innerException">Inner exception for the response</param>
    public ServiceRegistrationException(string message, string details, Exception innerException)
        : base(message, details, innerException)
    {
    }
}
