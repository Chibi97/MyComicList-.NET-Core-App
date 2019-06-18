using System;
using System.Linq;
using System.Collections.Generic;
using MyComicList.Application.Commands.Roles;
using MyComicList.Application.DataTransfer.Roles;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.Roles
{
    public class EFUpdateRole : EFBaseCommand, IUpdateRole
    {
        public EFUpdateRole(MyComicListContext context) : base(context)
        {
        }

        public void Execute(RoleDTO request)
        {
            var role = Context.Roles
                .Where(r => r.Id == request.Id && r.DeletedAt == null)
                .FirstOrDefault();

            if (role == null) throw new EntityNotFoundException("Role", request.Id);

            if(role.Name != null)
            {
                if (role.Name != request.Name)
                {
                    if (Context.Roles.Any(r => r.Name == request.Name))
                    {
                        throw new EntityAlreadyExistsException("Role", request.Name);
                    }

                    role.Name = request.Name;
                    role.UpdatedAt = DateTime.Now;

                    Context.SaveChanges();
                }
            }
            
        }
    }
}
