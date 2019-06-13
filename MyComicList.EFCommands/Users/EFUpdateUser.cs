﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyComicList.Application.Commands.Users;
using MyComicList.Application.DataTransfer.Users;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Helpers;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.Users
{
    public class EFUpdateUser : EFBaseCommand, IUpdateUser
    {
        public EFUpdateUser(MyComicListContext context) : base(context) { }

        public void Execute(UserUpdateDTO request)
        {
            var user = Context.Users
                .Where(u => u.Id == request.UserId && u.DeletedAt == null)
                .FirstOrDefault();

            if (user == null) throw new EntityNotFoundException("User", request.UserId);
            Mapper.Automap(request, user);
            user.UpdatedAt = DateTime.Now;
            if (request.Password != null)
            {
                string hashedPassword = MakeHashPassword(request.Password);
                user.Password = hashedPassword;

            }
            Context.SaveChanges();
        }
    }
}