﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Users;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.Users
{
    public class EFDeleteUser : EFBaseCommand, IDeleteUser
    {
        public EFDeleteUser(MyComicListContext context) : base(context) { }

        public void Execute(int id)
        {
            var user = Context.Users
                .Include(u => u.Comics)
                .Where(u => u.Id == id && u.DeletedAt == null)
                .FirstOrDefault();

            if (user == null) throw new EntityNotFoundException("User", id);
            user.DeletedAt = DateTime.Now;
            user.Email += "_Deleted_" + CurrentTimeStamp;
            user.Username += "_Deleted_" + CurrentTimeStamp;
            user.Comics.Clear();

            Context.SaveChanges();

        }
    }
}
