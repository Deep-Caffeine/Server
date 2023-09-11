using System.IdentityModel.Tokens.Jwt;
using server.Services;
using server.Utilities;

namespace server.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate mNext;
    private static Dictionary<long, DateTime> mUserBlackList;

    static JwtMiddleware()
    {
        mUserBlackList = new Dictionary<long, DateTime>();
    }

    public JwtMiddleware(RequestDelegate next)
    {
        mNext = next;
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

        await mNext(context);
    }

    public static void BanUser(long id, TimeSpan timeSpan)
    {
        mUserBlackList[id] = DateTime.UtcNow + timeSpan;
    }

    public static bool IsBannedUser(long id)
    {
        if (mUserBlackList.ContainsKey(id) && mUserBlackList[id] > DateTime.UtcNow)
        {
            return true;
        }

        return false;
    }
}

