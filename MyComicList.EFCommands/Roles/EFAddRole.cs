using System;
using System.Linq;
using System.Collections.Generic;
using MyComicList.Application.Commands.Roles;
using MyComicList.Application.DataTransfer.Roles;
using MyComicList.DataAccess;
using MyComicList.Application.Exceptions;
using MyComicList.Domain;

namespace MyComicList.EFCommands.Roles
{
    public class EFAddRole : EFBaseCommand, IAddRole
    {
        public EFAddRole(MyComicListContext context) : base(context)
        {
        }

        public void Execute(RoleAddDTO request)
        {
            if (Context.Roles.Any(c => c.Name == request.Name))
            {
                throw new EntityAlreadyExistsException("Name", request.Name);
            }

            Role newRole = new Role
            {
                Name = request.Name.Trim()
            };

            Context.Roles.Add(newRole);

            Context.SaveChanges();
        }
    }
}
