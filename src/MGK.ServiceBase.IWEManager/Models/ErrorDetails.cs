using Newtonsoft.Json;

namespace MGK.ServiceBase.IWEManager.Models;

public sealed class ErrorDetails
{
    public required int StatusCode { get; init; }

    public required string Message { get; init; }

    public required string Details { get; init; }

    public override string ToString()
        => JsonConvert.SerializeObject(this);
}
