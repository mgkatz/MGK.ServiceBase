﻿using MGK.ServiceBase.IWEManager.Infrastructure.Exceptions;
using System.Runtime.Serialization;

namespace MGK.ServiceBase.Services.Infrastructure.Exceptions;

[Serializable]
public class ServiceValidationException : BaseException
{
    protected ServiceValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Exception for validation exceptions on services.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    public ServiceValidationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Exception for validation exceptions on services.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    /// <param name="details">Details to show in response</param>
    public ServiceValidationException(string message, string details)
        : base(message, details)
    {
    }

    /// <summary>
    /// Exception for validation exceptions on services.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    /// <param name="details">Details to show in response</param>
    /// <param name="innerException">Inner exception for the response</param>
    public ServiceValidationException(string message, string details, Exception innerException)
        : base(message, details, innerException)
    {
    }
}
