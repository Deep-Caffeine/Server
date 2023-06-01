using System.IdentityModel.Tokens.Jwt;
using server.Services;

namespace server.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate mNext;

    public JwtMiddleware(RequestDelegate next)
    {
        mNext = next;
    }

    public async Task Invoke(HttpContext context, UserService userService, AuthService authService)
    {
        string? authorization = context.Request.Headers["Authorization"];

        if (authorization != null)
        {
            string token = authorization.Split(" ").Last();
            JwtSecurityToken? jwtToken = authService.ValidateToken(token);

            if (jwtToken != null)
            {
                context.Items["IsValidation"] = true;
                context.Items["JwtToken"] = jwtToken;
            }
        }

        await mNext(context);
    }
}

