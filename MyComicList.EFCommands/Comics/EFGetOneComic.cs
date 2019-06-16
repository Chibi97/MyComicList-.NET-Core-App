using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyComicList.EFCommands.MyListOfComics
{
    public class EFGetOneComic : EFBaseCommand, IGetOneComic
    {
        public EFGetOneComic(MyComicListContext context) : base(context) { }

        public ComicGetDTO Execute(int id)
        {
            var comic = Context.Comics
                        .Include(c => c.Publisher)
                        .Include(c => c.ComicGenres).ThenInclude(cg => cg.Genre)
                        .Include(c => c.ComicAuthors).ThenInclude(ca => ca.Author)
                        .Where(c => c.DeletedAt == null && c.Id == id)
                        .SingleOrDefault();

            if (comic == null) throw new EntityNotFoundException("Comic", id);
            return new ComicGetDTO()
            {
                Id = comic.Id,
                Name = comic.Name,
                Description = comic.Description,
                Issues = comic.Issues,
                PublishedAt = comic.PublishedAt.Date,
                Publisher = comic.Publisher.Name,
                Genres = comic.ComicGenres.Select(cg => cg.Genre.Name),
                Authors = comic.ComicAuthors.Select(ca => ca.Author.FirstName + ' ' + ca.Author.LastName)
            };
        }
    }
}

