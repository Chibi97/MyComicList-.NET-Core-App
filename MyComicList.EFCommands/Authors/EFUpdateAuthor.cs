using System;
using System.Linq;
using System.Collections.Generic;
using MyComicList.Application.Commands.Authors;
using MyComicList.Application.DataTransfer.Authors;
using MyComicList.DataAccess;
using MyComicList.Application.Exceptions;

namespace MyComicList.EFCommands.Authors
{
    public class EFUpdateAuthor : EFBaseCommand, IUpdateAuthor
    {
        public EFUpdateAuthor(MyComicListContext context) : base(context)
        {
        }

        public void Execute(AuthorUpdateDTO request)
        {
            var author = Context.Authors
                .Where(a => a.Id == request.Id && a.DeletedAt == null)
                .FirstOrDefault();

            if (author == null) throw new EntityNotFoundException("Author", request.Id);

            author.FirstName = request.FirstName;
            author.LastName = request.LastName;
            author.UpdatedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
