using System;
using System.Linq;
using MyComicList.Application.Commands.Users;
using MyComicList.Application.DataTransfer.Auth;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Helpers;
using MyComicList.Application.Interfaces;
using MyComicList.DataAccess;
using MyComicList.Domain;

namespace MyComicList.EFCommands.Users
{
    public class EFRegisterUser : EFBaseCommand, IRegisterUser
    {
        private readonly IEmailSender emailSender;

        public EFRegisterUser(MyComicListContext context, IEmailSender emailSender) : base(context)
        {
            this.emailSender = emailSender;
        }

        public void Execute(UserRegisterDTO request)
        {
            if (Context.Users.Any(u => u.Username == request.Username))
            {
                throw new EntityAlreadyExistsException("Username", request.Username);
            };

            if (Context.Users.Any(u => u.Email == request.Email))
            {
                throw new EntityAlreadyExistsException("Email", request.Email);
            };

            User user = new User
            {
                Role = Context.Roles.Where(r => r.Name == "User").SingleOrDefault()
            };

            Mapper.Automap(request, user);

            Context.Users.Add(user);
            Context.SaveChanges();

            emailSender.Subject = "Successfully added!";
            emailSender.Body = "Administrator has added you, welcome!";
            emailSender.ToEmail = user.Email;
            emailSender.Send();
        }
    }
}
