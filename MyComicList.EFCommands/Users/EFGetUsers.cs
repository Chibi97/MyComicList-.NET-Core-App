using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Users;
using MyComicList.Application.DataTransfer.Users;
using MyComicList.Application.Helpers;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;
using MyComicList.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyComicList.EFCommands.Users
{
    public class EFGetUsers : EFBaseCommand, IGetUsers
    {
        public EFGetUsers(MyComicListContext context) : base(context) { }

        public PagedResponse<UserGetDTO> Execute(UserRequest request)
        {
            var users = Context.Users.AsQueryable();

            if(request.Username != null)
            {
                users = users.Where(u => u.Username.ToLower().Contains(request.Username.Trim().ToLower()));
            }

            return users
                .Include(u => u.Comics).ThenInclude(uc => uc.Comic)
                .Where(u => u.DeletedAt == null)
                .Select(u => new UserGetDTO
                {
                    Id = u.Id,
                    Username = u.Username,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Comics = u.Comics.Select(cu => cu.Comic.Name)
                })
                .Paginate(request.PerPage, request.Page);
        }
    }
}
