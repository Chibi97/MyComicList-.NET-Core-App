using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyComicList.EFCommands.Comics
{
    public class EFGetOneComic : EFBaseCommand, IGetOneComic
    {
        public EFGetOneComic(MyComicListContext context) : base(context) { }

        public ComicDTO Execute(int id)
        {
            // var comic = Context.Comics.Find(id);
            var comic = Context.Comics
                        .Include(c => c.Publisher)
                        .Include(c => c.ComicGenres).ThenInclude(cg => cg.Genre)
                        .Include(c => c.ComicAuthors).ThenInclude(ca => ca.Author)
                        .SingleOrDefault(x => x.Id == id);

            if (comic == null) throw new EntityNotFoundException();
            return new ComicDTO()
            {
                Name = comic.Name,
                Description = comic.Description,
                Issues = comic.Issues,
                PublishedAt = comic.PublishedAt.Date,
                Publisher = comic.Publisher.Name,
                Genres = comic.ComicGenres.Select(cg => cg.Genre.Name),
                Authors = comic.ComicAuthors.Select(ca => ca.Author.FullName)
            };
        }
    }
}

