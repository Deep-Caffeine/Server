using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.DTOs;
using server.Entity;
using server.Services;


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

        [HttpGet]
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
        public ActionResult<KeyValueErrorResponse> Create()
        {
            KeyValueErrorResponse keyValueErrorResponse = new KeyValueErrorResponse { error = "Bad Request", email = false };
            return BadRequest(keyValueErrorResponse);
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
