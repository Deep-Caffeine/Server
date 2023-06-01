using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using server.Utilities;

namespace server.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RefreshAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // [AllowAnonymous] attribute가 있는 경우 스킵
        bool allowAnonymouse = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymouse)
        {
            return;
        }

        // 토큰 인증 진행 (Refresh Token만 통과)
        bool isValidation = (bool)(context.HttpContext.Items["IsValidation"] ?? false);
        JwtSecurityToken? jwtToken = (JwtSecurityToken?)context.HttpContext.Items["JwtToken"];

        if (!isValidation || jwtToken == null)
        {
            context.Result = new UnauthorizedResult();
        }
        else
        {
            string isRefreshOnly = jwtToken.GetClaimByType("refresh_only") ?? "false";

            if (isRefreshOnly != "true")
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}

