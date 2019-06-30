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
            var user = Context.Users
                .Where(u => u.Id == request.UserId && u.DeletedAt == null)
                .FirstOrDefault();

            if (user == null) throw new EntityNotFoundException("User", request.UserId);

            if (request.Username != null)
            {
                if (request.Username != user.Username)
                {
                    if (Context.Users.Any(u => u.Username == request.Username))
                    {
                        throw new EntityAlreadyExistsException("Username", request.Username);
                    };
                }
            }

            if (request.Email != null)
            {
                if (request.Email != request.Email)
                {
                    if (Context.Users.Any(u => u.Email == request.Email))
                    {
                        throw new EntityAlreadyExistsException("Email", request.Email);
                    };
                }
            }
            
            Mapper.Automap(request, user);
            user.UpdatedAt = DateTime.Now;

            if(request.Role != null)
            {
                var role = Context.Roles.FirstOrDefault(r => r.Id == request.Role && r.DeletedAt == null);
                user.Role = role ?? throw new EntityNotFoundException("Role", (int)request.Role);
            }

            Context.SaveChanges();
        }
    }
}
