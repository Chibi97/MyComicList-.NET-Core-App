using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyComicList.API.Middlewares;
using MyComicList.API.Services;
using MyComicList.Application.Commands.Authors;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.Commands.Genres;
using MyComicList.Application.Commands.MyList;
using MyComicList.Application.Commands.Publishers;
using MyComicList.Application.Commands.Roles;
using MyComicList.Application.Commands.Users;
using MyComicList.Application.DataTransfer;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Responses;
using MyComicList.DataAccess;
using MyComicList.EFCommands.Authors;
using MyComicList.EFCommands.Genres;
using MyComicList.EFCommands.MyListOfComics;
using MyComicList.EFCommands.Publishers;
using MyComicList.EFCommands.Roles;
using MyComicList.EFCommands.Users;
using Newtonsoft.Json;

namespace MyComicList.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<MyComicListContext>();
            services.AddTransient<IGetComics, EFGetComics>();
            services.AddTransient<IGetOneComic, EFGetOneComic>();
            services.AddTransient<IAddComic, EFAddComic>();
            services.AddTransient<IUpdateComic, EFUpdateComic>();
            services.AddTransient<IDeleteComic, EFDeleteComic>();

            services.AddTransient<IGetUsers, EFGetUsers>();
            services.AddTransient<IGetOneUser, EFGetOneUser>();
            services.AddTransient<IAddUser, EFAddUser>();
            services.AddTransient<IUpdateUser, EFUpdateUser>();
            services.AddTransient<IDeleteUser, EFDeleteUser>();

            services.AddTransient<IAddToMyList, EFAddToMyList>();
            services.AddTransient<IDeleteFromMyList, EFDeleteFromMyList>();
            services.AddTransient<IGetMyList, EFGetMyList>();
            services.AddTransient<IGetOneFromMyList, EFGetOneFromMyList>();

            services.AddTransient<IAddGenre, EFAddGenre>();
            services.AddTransient<IUpdateGenre, EFUpdateGenre>();
            services.AddTransient<IDeleteGenre, EFDeleteGenre>();

            services.AddTransient<IAddAuthor, EFAddAuthor>();
            services.AddTransient<IUpdateAuthor, EFUpdateAuthor>();
            services.AddTransient<IDeleteAuthor, EFDeleteAuthor>();

            services.AddTransient<IAddPublisher, EFAddPublisher>();
            services.AddTransient<IDeletePublisher, EFDeletePublisher>();
            services.AddTransient<IGetPublishers, EFGetPublishers>();
            services.AddTransient<IUpdatePublisher, EFUpdatePublisher>();

            services.AddTransient<IAddRole, EFAddRole>();
            //services.AddTransient<IDeleteRole, EFDeleteRole>();
            //services.AddTransient<IUpdateRole, EFUpdateRole>();

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ITokenService<int, UserLoginDTO>, JWTUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<LoginMiddleware>();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
