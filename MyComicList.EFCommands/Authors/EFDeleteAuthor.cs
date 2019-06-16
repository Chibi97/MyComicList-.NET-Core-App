using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Authors;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.Authors
{
    public class EFDeleteAuthor : EFBaseCommand, IDeleteAuthor
    {
        public EFDeleteAuthor(MyComicListContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var author = Context.Authors
                .Include(a => a.ComicAuthors)
                .Where(a => a.DeletedAt == null && a.Id == id)
                .FirstOrDefault();

            if (author == null) throw new EntityNotFoundException("Author", id);

            author.DeletedAt = DateTime.Now;
            author.ComicAuthors.Clear();
            Context.SaveChanges();
        }
    }
}
