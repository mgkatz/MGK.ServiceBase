using MGK.ServiceBase.Configuration.Infrastructure.Extensions;
using MGK.ServiceBase.IWEManager.Infrastructure.Extensions;
using MGK.ServiceBase.Messaging.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace MGK.ServiceBase.Messaging;

public sealed class SmtpObject : ISmtpObject, IDisposable
{
    private readonly SmtpOptions _smtpOptions;
    private SmtpClient _client;

    public SmtpObject(
        IOptions<SmtpOptions> smtpOptions,
        ILogger<SmtpObject> logger)
    {
        ValidateConstructorParams(smtpOptions, logger);

        _smtpOptions = smtpOptions.Value;
    }

    public SmtpClient Client
    {
        get
        {
            return _client ??= new SmtpClient()
            {
                Credentials = new NetworkCredential(_smtpOptions.UserName, _smtpOptions.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = _smtpOptions.EnableSsl,
                Host = _smtpOptions.Client,
                Port = _smtpOptions.Port,
                Timeout = _smtpOptions.Timeout,
                UseDefaultCredentials = false
            };
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Free managed resources
            _client?.Dispose();
        }

        // Free native resources if there are any.
    }

    private static void ValidateConstructorParams(
        IOptions<SmtpOptions> smtpOptions,
        ILogger<SmtpObject> logger)
    {
        logger.EnsureIsAlive();
        smtpOptions.EnsureIsAlive(logger);
    }
}
