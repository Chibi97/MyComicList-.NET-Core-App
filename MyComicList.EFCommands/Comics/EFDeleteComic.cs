using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;
using System;
using System.Linq;

namespace MyComicList.EFCommands.MyListOfComics
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
               .Include(c => c.Pictures)
               .Where(c => c.Id == id && c.DeletedAt == null)
               .FirstOrDefault();

            if (comic == null) throw new EntityNotFoundException("Comic", id);

            comic.DeletedAt = DateTime.Now;
            comic.Name += "_Deleted_" + CurrentTimeStamp;

            comic.ComicGenres.Clear();
            comic.ComicAuthors.Clear();
            comic.Users.Clear();
            comic.Pictures.Clear();

            Context.SaveChanges();
        }
    }
}
