using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;
using System;
using System.Linq;

namespace MyComicList.EFCommands.Comics
{
    public class EFDeleteComic : EFBaseCommand, IDeleteComic
    {
        public EFDeleteComic(MyComicListContext context) : base(context) { }

        public void Execute(int id)
        {
            var comic = Context.Comics
               .Include(c => c.ComicGenres)
               .Include(c => c.ComicAuthors)
               .Include(c => c.Users)
               .Include(c => c.Reviews)
               .Where(c => c.Id == id && c.DeletedAt == null)
               .FirstOrDefault();

            if (comic == null) throw new EntityNotFoundException("Comic", id);

            comic.DeletedAt = DateTime.Now;

            comic.ComicGenres.Clear();
            comic.ComicAuthors.Clear();
            comic.Users.Clear();
            comic.Reviews.Clear();

            Context.SaveChanges();
        }
    }
}
