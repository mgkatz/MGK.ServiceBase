using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MGK.ServiceBase.Security.SeedWork;

public interface IJwtService : ISecurityService
{
    string GenerateRefreshToken();
    JwtSecurityToken GenerateToken(Claim[] authClaims = null);
    bool TryGetPrincipalFromToken(string token, out ClaimsPrincipal claimsPrincipal);
}
