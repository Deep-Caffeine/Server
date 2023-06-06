using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using server.Attributes;
using server.DTOs;
using server.Entities;
using server.Services;
using server.Utilities;

namespace server.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService mUserService;

        public UserController(UserService userService)
        {
            this.mUserService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GetUserResponse>> Read()
        {
            JwtSecurityToken jwtToken = HttpContext.GetJwtToken();
            long id = long.Parse(jwtToken.GetClaimByType("id"));

            var userResponse = await mUserService.Read(id);

            if (userResponse == null)
            {
                return Unauthorized();
            }

            return Ok(userResponse);
        }

        [HttpPost]
        public async Task<ActionResult<KeyValueErrorResponse>> Create([FromBody] CreateUserRequest body)
        {
            try
            {
                bool result = await this.mUserService.Create(body);
                if (result == false)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(new
                {
                    status = 400, title = "This is a duplicate email.", errors = new { Email = "중복된 이메일 입니다." }
                });
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<KeyValueErrorResponse>> Update([FromBody] PutUserRequest model)
        {
            JwtSecurityToken jwtToken = HttpContext.GetJwtToken();
            long id = long.Parse(jwtToken.GetClaimByType("id"));

            bool userResponse = await mUserService.Update(id, model);

            if (!userResponse)
            {
                return Unauthorized();
            }

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public ActionResult Delete()
        {
            return Unauthorized();
        }
    }
}
