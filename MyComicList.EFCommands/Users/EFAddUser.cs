using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MyComicList.Application.Commands.Users;
using MyComicList.Application.DataTransfer.Users;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Helpers;
using MyComicList.Application.Interfaces;
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
        private readonly IEmailSender emailSender;

        public EFAddUser(MyComicListContext context, IEmailSender emailSender) : base(context) {
            this.emailSender = emailSender;
        }

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

            var role = Context.Roles.FirstOrDefault(r => r.Id == request.Role && r.DeletedAt == null);
            if (role == null) throw new EntityNotFoundException("Role", request.Role);

            string hashedPassword = MakeHashPassword(request.Password);
            User user = new User
            {
                Password = hashedPassword,
                Role = role
            };

            Mapper.Automap(request, user);

            Context.Users.Add(user);
            Context.SaveChanges();

            emailSender.Subject = "Successfull registration!";
            emailSender.Body = "You are succesfully registered!";
            emailSender.ToEmail = user.Email;
            emailSender.Send();
        }

    }
}
