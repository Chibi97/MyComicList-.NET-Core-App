using System;
using System.Linq;
using System.Collections.Generic;
using MyComicList.Application.Commands.Authors;
using MyComicList.Application.DataTransfer.Authors;
using MyComicList.DataAccess;
using MyComicList.Domain;

namespace MyComicList.EFCommands.Authors
{
    public class EFAddAuthor : EFBaseCommand, IAddAuthor
    {
        public EFAddAuthor(MyComicListContext context) : base(context)
        {
        }

        public void Execute(AuthorAddDTO request)
        {
            Author newAuthor = new Author
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim()
            };

            Context.Authors.Add(newAuthor);

            Context.SaveChanges();
        }
    }
}
