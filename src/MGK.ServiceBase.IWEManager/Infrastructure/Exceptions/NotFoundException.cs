﻿using System.Runtime.Serialization;

namespace MGK.ServiceBase.IWEManager.Infrastructure.Exceptions;

[Serializable]
public class NotFoundException : BaseException
{
    protected NotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Exception for not found issues.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    public NotFoundException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Exception for not found issues.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    /// <param name="details">Details to show in response</param>
    public NotFoundException(string message, string details)
        : base(message, details)
    {
    }

    /// <summary>
    /// Exception for not found issues.
    /// </summary>
    /// <param name="message">Message to show in response</param>
    /// <param name="details">Details to show in response</param>
    /// <param name="innerException">Inner exception for the response</param>
    public NotFoundException(string message, string details, Exception innerException)
        : base(message, details, innerException)
    {
    }
}
