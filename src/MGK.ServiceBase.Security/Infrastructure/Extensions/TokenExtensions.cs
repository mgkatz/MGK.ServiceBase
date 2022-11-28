using System.IdentityModel.Tokens.Jwt;

namespace MGK.ServiceBase.Security.Infrastructure.Extensions;

public static class TokenExtensions
{
    public static DateTime ExpiresOn(this string token)
    {
        var jwtSecurityToken = new JwtSecurityToken(token);
        var tokenExp = jwtSecurityToken.Claims.First(c => c.Type.Equals("exp")).Value;
        var ticks = long.Parse(tokenExp);

        return DateTimeOffset.FromUnixTimeSeconds(ticks).UtcDateTime;
    }

    public static bool HasExpired(this string token)
        => ExpiresOn(token) < DateTime.UtcNow;

    public static bool IsValidUser(this string token, string userIdentityId)
    {
        var jwtSecurityToken = new JwtSecurityToken(token);
        var userIdentityIdInClaim = jwtSecurityToken.Claims
            .FirstOrDefault(c => c.Type.Equals(nameof(userIdentityId), StringComparison.OrdinalIgnoreCase))
            .Value;

        return userIdentityId.Equals(userIdentityIdInClaim, StringComparison.OrdinalIgnoreCase);
    }

    public static string WriteToken(this JwtSecurityToken source)
        => new JwtSecurityTokenHandler().WriteToken(source);
}
