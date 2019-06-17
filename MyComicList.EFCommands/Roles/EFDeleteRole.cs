using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Roles;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.Roles
{
    public class EFDeleteRole : EFBaseCommand, IDeleteRole
    {
        public EFDeleteRole(MyComicListContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var role = Context.Roles
                .Include(r => r.Users)
                .Where(r => r.DeletedAt == null && r.Id == id)
                .FirstOrDefault();

            if (role == null) throw new EntityNotFoundException("Role", id);

            if (!role.Users.Any())
            {
                role.DeletedAt = DateTime.Now;
                role.Name += "_Deleted_" + CurrentTimeStamp;
                Context.SaveChanges();
            }
            else throw new NotEmptyCollectionException("Role", "comics");
        }
    }
}
