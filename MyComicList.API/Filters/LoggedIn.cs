﻿using Microsoft.AspNetCore.Mvc.Filters;
using MyComicList.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyComicList.API.Filters
{
    public class LoggedIn : Attribute, IResourceFilter
    {
        private readonly string role;
        public LoggedIn(string role)
        {
            this.role = role;
        }

        public LoggedIn()
        {
            this.role = "All";
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var service = context.HttpContext.RequestServices.GetService<ILoginService>();
            var user = service.PossibleUser();

            if (user == null)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                if (role != null)
                {
                    if (role == "All") return;

                    if (!user.Role.Name.Equals(role) )
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
            }
        }
    }
}
