using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using server.Attributes;
using server.DTOs;
using server.Middlewares;
using server.Services;
using server.Utilities;

namespace server.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<KeyValueErrorResponse>> Create([FromBody] CreateUserRequest body)
        {
            try
            {
                AuthResponse? result = await this._userService.Create(body);
                /*if (result == null)
                {
                    return BadRequest();
                }*/
                return Ok(result);
            }
            catch (Exception error)
            {
                return BadRequest(new
                {
                    status = 400,
                    title = "This is a duplicate email.",
                    errors = new { Email = "중복된 이메일 입니다." }
                });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GetUserResponse>> Read()
        {
            JwtSecurityToken jwtToken = HttpContext.GetJwtToken();
            long id = long.Parse(jwtToken.GetClaimByType("id"));

            var userResponse = await _userService.Read(id);

            if (userResponse == null)
            {
                return Unauthorized();
            }

            return Ok(userResponse);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<KeyValueErrorResponse>> Update([FromBody] PutUserRequest model)
        {
            JwtSecurityToken jwtToken = HttpContext.GetJwtToken();
            long id = long.Parse(jwtToken.GetClaimByType("id"));

            bool userResponse = await _userService.Update(id, model);

            if (!userResponse)
            {
                return Unauthorized();
            }

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete()
        {
            JwtSecurityToken jwtToken = HttpContext.GetJwtToken();
            long id = long.Parse(jwtToken.GetClaimByType("id"));

            await _userService.Delete(id);

            JwtMiddleware.BanUser(id, TimeSpan.FromDays(28));

            return Ok();
        }
    }
}
