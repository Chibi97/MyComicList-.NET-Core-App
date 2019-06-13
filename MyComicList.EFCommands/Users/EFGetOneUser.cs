using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Users;
using MyComicList.Application.DataTransfer.Users;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Helpers;
using MyComicList.Application.Requests;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.Users
{
    public class EFGetOneUser : EFBaseCommand, IGetOneUser
    {
        public EFGetOneUser(MyComicListContext context) : base(context) {}

        public UserGetDTO Execute(int id)
        {
            var user = Context.Users
                .Include(u => u.Comics).ThenInclude(cu => cu.Comic)
                .Where(u => u.DeletedAt == null && u.Id == id)
                .SingleOrDefault();

            if (user == null) throw new EntityNotFoundException("User", id);

            return new UserGetDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Comics = user.Comics.Select(cu => cu.Comic.Name)
            };
        }
    }
}
