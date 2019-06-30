using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyComicList.API.Services;
using MyComicList.Application.Commands.Users;
using MyComicList.Application.DataTransfer.Auth;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Responses;

namespace MyComicList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService<int, UserLoginDTO> tokenService;
        private readonly IPasswordService passwordService;
        private readonly IRegisterUser registerCommand;

        public AuthController(ITokenService<int, UserLoginDTO> tokenService, IPasswordService passwordService, IRegisterUser registerCommand)
        {
            this.tokenService = tokenService;
            this.passwordService = passwordService;
            this.registerCommand = registerCommand;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDTO request)
        {
            try
            {
                var token = tokenService.Encrypt(request);
                if (token == null) return Unauthorized();
                return Ok(new MessageResponse()
                {
                    Message = token
                });
            }
            catch(UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDTO request)
        {
            try
            {
                string passwordValue = request.Password;
                request.Password = passwordService.HashPassword(passwordValue);
                registerCommand.Execute(request);

                request.Password = passwordValue;
                var token = tokenService.Encrypt(request);

                return Ok(new MessageResponse()
                {
                    Message = token
                });

            } catch(EntityAlreadyExistsException e)
            {
                return Conflict(new ErrorMessage { Message = e.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            
        }

    }
}