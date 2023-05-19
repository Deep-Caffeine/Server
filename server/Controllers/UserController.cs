using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.DTOs;
using server.Entity;

namespace server.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<GetUserResponse>> Get()
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
        public async Task<ActionResult<KeyValueErrorResponse>> Create()
        {
            KeyValueErrorResponse keyValueErrorResponse = new KeyValueErrorResponse { error = "Bad Request", email = false };
            return BadRequest(keyValueErrorResponse);
        }
        
        [HttpPut]
        public async Task<ActionResult<KeyValueErrorResponse>> Update()
        {
            return Ok();
        }

        private bool UserEntityExists(long id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
