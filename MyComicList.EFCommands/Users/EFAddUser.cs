using MyComicList.Application.Commands.Users;
using MyComicList.Application.DataTransfer.Users;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Helpers;
using MyComicList.Application.Interfaces;
using MyComicList.DataAccess;
using MyComicList.Domain;
using System;
using System.Linq;

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

            User user = new User
            {
                Role = role
            };

            Mapper.Automap(request, user);

            Context.Users.Add(user);
            Context.SaveChanges();

            emailSender.Subject = "Successfull registration!";
            emailSender.Body = "You have been sucessfully registered, welcome!";
            emailSender.ToEmail = user.Email;
            emailSender.Send();
        }

    }
}
