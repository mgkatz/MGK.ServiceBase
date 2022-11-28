using MGK.ServiceBase.Messaging.Infrastructure.Enums;
using MGK.ServiceBase.Messaging.Infrastructure.Exceptions;

namespace MGK.ServiceBase.Messaging.SeedWork;

public interface ISendMessageResult
{
    SendEmailStatus Status { get; init; }
    SendMessageException Error { get; init; }
    byte NumberOfRetriesMade { get; init; }
}
