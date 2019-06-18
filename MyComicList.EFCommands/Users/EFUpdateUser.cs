using System;
using System.Linq;
using System.Collections.Generic;
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
            if (request.Username != null)
            {
                if (Context.Users.Any(u => u.Username == request.Username))
                {
                    throw new EntityAlreadyExistsException("Username", request.Username);
                };
            }

            if(request.Email != null)
            {
                if (Context.Users.Any(u => u.Email == request.Email))
                {
                    throw new EntityAlreadyExistsException("Email", request.Email);
                };
            }

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

            if(request.Role != null)
            {
                var role = Context.Roles.FirstOrDefault(r => r.Id == request.Role && r.DeletedAt == null);
                user.Role = role ?? throw new EntityNotFoundException("Role", (int)request.Role);
            }

            Context.SaveChanges();
        }
    }
}
