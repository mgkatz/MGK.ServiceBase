﻿using MGK.ServiceBase.IWEManager.Infrastructure.Exceptions;
using System.Runtime.Serialization;

namespace MGK.ServiceBase.API.Infrastructure.Exceptions;

[Serializable]
public class ApplicationValidationException : BaseException
{
    protected ApplicationValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Exception for validation exceptions on APIs.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    public ApplicationValidationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Exception for validation exceptions on APIs.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    /// <param name="details">Details to show in response</param>
    public ApplicationValidationException(string message, string details)
        : base(message, details)
    {
    }

    /// <summary>
    /// Exception for validation exceptions on APIs.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    /// <param name="details">Details to show in response</param>
    /// <param name="innerException">Inner exception for the response</param>
    public ApplicationValidationException(string message, string details, Exception innerException)
        : base(message, details, innerException)
    {
    }
}
