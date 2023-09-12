using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using server.Attributes;
using server.DTOs;
using server.Entities;
using server.Services;
using server.Utilities;

namespace server.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult<AuthResponse> Auth([FromBody] AuthRequest authRequest)
    {
        UserEntity? userEntity = _authService.UserAuthorize(authRequest.email, authRequest.password);

        AuthResponse response = new AuthResponse();

        if (userEntity == null)
        {
            // Unauthorized
            return Unauthorized();
        }
        else if (authRequest.no_token ?? false)
        {
            // No token response
            return response;
        }
        else
        {
            response.access_token = _authService.GenerateAccessToken(userEntity.Id);
            response.refresh_token = _authService.GenerateRefreshToken(userEntity.Id);

            return response;
        }
    }

    [Route("refresh")]
    [HttpGet]
    [RefreshAuthorize]
    public ActionResult<AuthResponse> Refresh()
    {
        JwtSecurityToken jwtToken = HttpContext.GetJwtToken();
        long id = long.Parse(jwtToken.GetClaimByType("id"));

        AuthResponse response = new AuthResponse();
        response.access_token = _authService.GenerateAccessToken(id);

        return response;
    }
}
