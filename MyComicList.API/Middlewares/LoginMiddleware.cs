using Microsoft.AspNetCore.Http;
using MyComicList.API.Services;
using MyComicList.Application.DataTransfer;
using MyComicList.Application.Exceptions;
using System;
using System.Threading.Tasks;

namespace MyComicList.API.Middlewares
{
    public class LoginMiddleware
    {
        private readonly RequestDelegate next;
        public LoginMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILoginService loginService, ITokenService<int, UserLoginDTO> tokenService)
        {
            var token = httpContext.Request.Headers["Authorization"].ToString();
            if(!string.IsNullOrEmpty(token))
            {
                try
                {
                    int id = tokenService.Decrypt(token);
                    loginService.Login(id);
                    await next(httpContext);

                } catch(InvalidTokenException e)
                {
                    await httpContext.Response.WriteAsync(e.Message);
                }
            } else
            {
                await next(httpContext);
            }
        }
    }
}
