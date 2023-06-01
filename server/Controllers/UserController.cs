using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Attributes;
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
        [Authorize]
        public ActionResult<GetUserResponse> Get()
        {
            GetUserResponse getUserResponse = new GetUserResponse
            {
                email = "von0401@deu.ac.kr",
                username = "Eun Jung Von",
                phone = "010-1234-5678",
                birth = "2014-04-01",
                profile_url = "/image/trolls",
                level = 418,
                sns = new string[3] { "kakao", "naver", "google" }
            };
            return Ok(getUserResponse);
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
        public ActionResult<KeyValueErrorResponse> Update()
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return Unauthorized();
        }
    }
}
