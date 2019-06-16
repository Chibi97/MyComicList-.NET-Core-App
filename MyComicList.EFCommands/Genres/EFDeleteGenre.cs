using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Genres;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.Genres
{
    public class EFDeleteGenre : EFBaseCommand, IDeleteGenre
    {
        public EFDeleteGenre(MyComicListContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var genre = Context.Genres
                .Include(g => g.ComicGenres)
                .Where(g => g.DeletedAt == null && g.Id == id)
                .FirstOrDefault();

            if (genre == null) throw new EntityNotFoundException("Genre", id);

            genre.DeletedAt = DateTime.Now;
            genre.ComicGenres.Clear();
            Context.SaveChanges();
        }
    }
}
