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
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.Commands.Users;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Responses;
using MyComicList.DataAccess;
using MyComicList.EFCommands.Comics;
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


            //app.UseExceptionHandler(builder =>
            //{
            //    builder.Run(async context =>
            //    {
            //        var feature = context.Features.Get<IExceptionHandlerFeature>();
            //        Exception exception = feature.Error;
            //        bool handled = false;

            //        if (exception is EntityAlreadyExistsException)
            //        {
            //            handled = true;
            //            context.Response.StatusCode = 409;
            //            context.Response.ContentType = "application/json";
            //            var error = new ErrorMessage() { Message = exception.Message };
            //            await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
            //        }

            //        if (exception is EntityNotFoundException)
            //        {
            //            handled = true;
            //            context.Response.StatusCode = 404;
            //            context.Response.ContentType = "application/json";
            //            var error = new ErrorMessage() { Message = exception.Message };
            //            await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
            //        }

            //        if (!handled)
            //        {
            //            await context.Response.WriteAsync("Please contact administrator for this error");
            //        }
            //    });
            //});

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
