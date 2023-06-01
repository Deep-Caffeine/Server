using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.DTOs;
using server.Entities;
using server.Services;


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
        public async Task<ActionResult<GetUserResponse>> Read([FromHeader(Name = "Id")] long id)
        {
            var userResponse = await mUserService.Read(id);

            if (userResponse == null)
            {
                return Unauthorized();
            }

            return Ok(userResponse);
        }

        [HttpPost]
        public ActionResult<KeyValueErrorResponse> Create([FromBody] UserEntity model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<KeyValueErrorResponse>> Update([FromHeader(Name = "Id")] long id, [FromBody] PutUserRequest model)
        {
            var userResponse = await mUserService.Update(id, model);
            
            if (userResponse == null)
            {
                return Unauthorized();
            }
            
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return Unauthorized();
        }
    }
}
