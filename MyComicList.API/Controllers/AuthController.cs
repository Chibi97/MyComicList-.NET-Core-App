using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Users;
using MyComicList.Application.DataTransfer.Auth;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Responses;
using MyComicList.DataAccess;
using MyComicList.Shared.Services;

namespace MyComicList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService<int, UserLoginDTO> tokenService;
        private readonly IPasswordService passwordService;
        private readonly IRegisterUser registerCommand;

        public AuthController(ITokenService<int, UserLoginDTO> tokenService, IPasswordService passwordService, IRegisterUser registerCommand, MyComicListContext context)
        {
            this.tokenService = tokenService;
            this.passwordService = passwordService;
            this.registerCommand = registerCommand;
            Context = context;
        }

        public MyComicListContext Context { get; }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDTO request)
        {
            try
            {
                var token = tokenService.Encrypt(request);
                if (token == null) return Unauthorized();

                return Ok(new AuthorizedUserResponse()
                {
                    Message = token,
                    Role = Context.Users.Include(u => u.Role).Where(u => u.Username.Equals(request.Username)).FirstOrDefault().Role.Name
                });
            }
            catch(UnauthorizedAccessException e)
            {
                return Unauthorized(new ErrorMessage { Message = "Wrong credentials" });
            }
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDTO request)
        {
            try
            {
                string passwordValue = request.Password;
                request.Password = passwordService.HashPassword(passwordValue);
                var response = registerCommand.Execute(request);

                request.Password = passwordValue;
                var token = tokenService.Encrypt(request);
                response.Message = token;

                return Ok(response);

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