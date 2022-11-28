using MGK.ServiceBase.Messaging.Infrastructure.Enums;
using MGK.ServiceBase.Messaging.Infrastructure.Exceptions;

namespace MGK.ServiceBase.Messaging;

public sealed record SendMessageResult : ISendMessageResult
{
    public SendEmailStatus Status { get; init; }
    public SendMessageException Error { get; init; }
    public byte NumberOfRetriesMade { get; init; }
}
