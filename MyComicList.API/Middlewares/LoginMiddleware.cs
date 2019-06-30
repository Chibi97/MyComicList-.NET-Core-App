using Microsoft.AspNetCore.Http;
using MyComicList.API.Services;
using MyComicList.Application.DataTransfer.Auth;
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
            httpContext.Response.Headers["Access-Control-Allow-Origin"] = "*";
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
                } catch(UnauthorizedAccessException)
                {
                    await httpContext.Response.WriteAsync("Unauthorized for this action.");
                }
            } else
            {
                await next(httpContext);
            }
        }
    }
}
