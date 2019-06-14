using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyComicList.API.Services;
using MyComicList.Application.DataTransfer;
using MyComicList.Application.Responses;

namespace MyComicList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService<int, UserLoginDTO> tokenService;

        public AuthController(ITokenService<int, UserLoginDTO> tokenService)
        {
            this.tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Login(UserLoginDTO request)
        {
            var token = tokenService.Encrypt(request);
            if (token == null) return Unauthorized();

            return Ok(new MessageResponse()
            {
                Message = token
            });
        }
    }
}