using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using server.Attributes;
using server.DTOs;
using server.Services;
using server.Utilities;

namespace server.Controllers;

[Route("chat")]
[ApiController]
public class ChatHttpController : ControllerBase
{
    public readonly ChatHttpService _chatHttpService;

    public ChatHttpController(ChatHttpService chatHttpService)
    {
        _chatHttpService = chatHttpService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Join([FromBody] ChatJoinRequest chatJoinRequest)
    {
        JwtSecurityToken jwtToken = HttpContext.GetJwtToken();
        long id = long.Parse(jwtToken.GetClaimByType("id"));

        bool? result = await _chatHttpService.Join(id, chatJoinRequest);

        if (result == null)
        {
            return NotFound();
        }

        return Ok();
    }
}
