﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace MGK.ServiceBase.IWEManager.Infrastructure.Exceptions;

[Serializable]
[SuppressMessage("Design", "RCS1194:Implement exception constructors.", Justification = "Only some specific constructors are needed.")]
public abstract class BaseException : Exception
{
    protected BaseException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    protected BaseException(string message)
        : base(message)
    {
    }

    protected BaseException(string message, string details)
        : base(message)
    {
        Details = details;
    }

    protected BaseException(string message, string details, Exception innerException)
        : base(message, innerException)
    {
        Details = details;
    }

    public string Details { get; } = string.Empty;
}
