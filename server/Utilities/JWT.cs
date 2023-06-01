using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace server.Utilities;

public static class JWT
{
    public static string? GetClaimByType(this JwtSecurityToken jwtToken, string typeName)
    {
        Claim? result = jwtToken.Claims.FirstOrDefault(x => x.Type == typeName);
        return result == null ? null : result.Value;
    }
}

