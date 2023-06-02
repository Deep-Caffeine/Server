using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace server.Utilities;

public static class JWT
{
    public static JwtSecurityToken GetJwtToken(this HttpContext httpContext)
    {
        Object? jwtToken = httpContext.Items["JwtToken"];

        if (jwtToken == null)
        {
            throw new Exception("Unable to get the JWT Token. The GetJwtToken method is recommended to be used with Authorize or RefreshAuthorize.");
        }
        else
        {
            return (JwtSecurityToken)jwtToken;
        }
    }

    public static string GetClaimByType(this JwtSecurityToken jwtToken, string typeName)
    {
        Claim? result = jwtToken.Claims.FirstOrDefault(x => x.Type == typeName);
        return result == null ? "" : result.Value;
    }
}

