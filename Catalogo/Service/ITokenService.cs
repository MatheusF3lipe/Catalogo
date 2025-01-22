using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Catalogo.Service
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims,IConfiguration config);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token,IConfiguration _config);
    }
}
