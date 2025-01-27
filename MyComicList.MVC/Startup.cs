﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyComicList.Shared.Services;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.Commands.Users;
using MyComicList.Application.Helpers;
using MyComicList.Application.Interfaces;
using MyComicList.DataAccess;
using MyComicList.EFCommands.MyListOfComics;
using MyComicList.EFCommands.Users;

namespace MyComicList.MVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<MyComicListContext>();

            // My Email sender
            var section = Configuration.GetSection("Email");

            var sender =
                new SmtpEmailSender(section["host"],
                Int32.Parse(section["port"]), section["fromaddress"], section["password"]);
            services.AddSingleton<IEmailSender>(sender);
            services.AddScoped<IPasswordService, BcryptNet>();

            // Comics
            services.AddTransient<IGetComics, EFGetComics>();
            services.AddTransient<IGetOneComic, EFGetOneComic>();
            services.AddTransient<IAddComic, EFAddComic>();
            services.AddTransient<IUpdateComic, EFUpdateComic>();
            services.AddTransient<IDeleteComic, EFDeleteComic>();

            // Users
            services.AddTransient<IGetUsers, EFGetUsers>();
            services.AddTransient<IGetOneUser, EFGetOneUser>();
            services.AddTransient<IAddUser, EFAddUser>();
            services.AddTransient<IUpdateUser, EFUpdateUser>();
            services.AddTransient<IDeleteUser, EFDeleteUser>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
