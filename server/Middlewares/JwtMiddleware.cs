using System.IdentityModel.Tokens.Jwt;
using server.Services;
using server.Utilities;

namespace server.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private static Dictionary<long, DateTime> _userBlackList;

    static JwtMiddleware()
    {
        _userBlackList = new Dictionary<long, DateTime>();
    }

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, AuthService authService)
    {
        string? authorization = context.Request.Headers["Authorization"];

        if (authorization != null)
        {
            string token = authorization.Split(" ").Last();
            JwtSecurityToken? jwtToken = authService.ValidateToken(token);

            if (jwtToken != null)
            {
                long id = long.Parse(jwtToken.GetClaimByType("id"));
                if (!JwtMiddleware.IsBannedUser(id))
                {
                    context.Items["IsValidation"] = true;
                    context.Items["JwtToken"] = jwtToken;
                }
            }
        }

        await _next(context);
    }

    public static void BanUser(long id, TimeSpan timeSpan)
    {
        _userBlackList[id] = DateTime.UtcNow + timeSpan;
    }

    public static bool IsBannedUser(long id)
    {
        if (_userBlackList.ContainsKey(id) && _userBlackList[id] > DateTime.UtcNow)
        {
            return true;
        }

        return false;
    }
}

