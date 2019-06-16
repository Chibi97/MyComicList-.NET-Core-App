﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MyComicList.Application.Commands.Users;
using MyComicList.Application.DataTransfer.Users;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Helpers;
using MyComicList.DataAccess;
using MyComicList.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.EFCommands.Users
{
    public class EFAddUser : EFBaseCommand, IAddUser
    {
        public EFAddUser(MyComicListContext context) : base(context) { }

        public void Execute(UserAddDTO request)
        {
            if (Context.Users.Any(u => u.Username == request.Username))
            {
                throw new EntityAlreadyExistsException("Username", request.Username);
            };

            if (Context.Users.Any(u => u.Email == request.Email))
            {
                throw new EntityAlreadyExistsException("Email", request.Email);
            };

            string hashedPassword = MakeHashPassword(request.Password);
            User user = new User
            {
                Password = hashedPassword
            };

            Mapper.Automap(request, user);

            Context.Users.Add(user);
            Context.SaveChanges();
        }

    }
}
