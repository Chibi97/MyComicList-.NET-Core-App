using Microsoft.AspNetCore.Http;
using MyComicList.Shared.Services;
using MyComicList.Application.DataTransfer.Auth;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

                } catch(UnauthorizedAccessException)
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsync(
                        JsonConvert.SerializeObject(
                            new { Message = "The token you provided is invalid." }
                        ));
                }
            } else
            {
                await next(httpContext);
            }
        }
    }
}
