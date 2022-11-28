namespace MGK.ServiceBase.Security.Infrastructure.Options;

public class JwtAuthOptions
{
    public const string OptionsKey = "JwtAuth";

    public string Audience { get; set; }
    public int Expiration { get; set; }
    public string Issuer { get; set; }
    public string Key { get; set; }
    public int Refresh { get; set; }
}
