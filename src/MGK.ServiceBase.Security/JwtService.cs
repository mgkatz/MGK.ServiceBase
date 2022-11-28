using MGK.ServiceBase.IWEManager.Infrastructure.Extensions;
using MGK.ServiceBase.Security.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MGK.ServiceBase.Security;

public class JwtService : IJwtService
{
    private readonly JwtAuthOptions _jwtAuthOptions;
    private readonly ILogger<JwtService> _logger;

    public JwtService(
        IOptions<JwtAuthOptions> jwtAuthOptions,
        ILogger<JwtService> logger)
    {
        _jwtAuthOptions = jwtAuthOptions.Value;
        _logger = logger;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public JwtSecurityToken GenerateToken(Claim[] authClaims = null)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthOptions.Key));

        return new JwtSecurityToken(
            issuer: _jwtAuthOptions.Issuer,
            audience: _jwtAuthOptions.Audience,
            claims: authClaims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_jwtAuthOptions.Expiration),
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
    }

    public bool TryGetPrincipalFromToken(string token, out ClaimsPrincipal claimsPrincipal)
    {
        claimsPrincipal = null;
        var isValidClaimsPrincipal = false;
        JwtSecurityTokenHandler tokenValidator = new();

        var parameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthOptions.Key)),
            ValidateLifetime = false
        };

        try
        {
            claimsPrincipal = tokenValidator.ValidateToken(token, parameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                _logger.LogError(SecurityResources.MessagesResources.TokenValidationError);
            }
            else
            {
                isValidClaimsPrincipal = true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(SecurityResources.MessagesResources.TokenValidationError + $"({ex.GetExceptionMesssages()})");
        }

        return isValidClaimsPrincipal;
    }
}
