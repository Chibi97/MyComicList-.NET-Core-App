﻿using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Helpers;
using MyComicList.DataAccess;
using MyComicList.Domain;
using System;
using System.Linq;

namespace MyComicList.EFCommands.Comics
{
    public class EFUpdateComic : EFBaseCommand, IUpdateComic
    {

        public EFUpdateComic(MyComicListContext context) : base(context) { }

        public void Execute(ComicUpdateDTO request)
        {
            var comic = Context.Comics
                .Include(c => c.Publisher)
                .Include(c => c.ComicGenres).ThenInclude(cg => cg.Genre)
                .Include(c => c.ComicAuthors).ThenInclude(ca => ca.Author)
                .Where(c => c.Id == request.ComicId)
                .FirstOrDefault();

            if (comic == null) throw new EntityNotFoundException("Comic");
            Mapper.Automap(request, comic);
            comic.UpdatedAt = DateTime.Now;

            if (request.Publisher != null)
            {
                var foundPublisher = Context.Publishers.FirstOrDefault(p => p.Id == request.Publisher);
                comic.Publisher = foundPublisher ?? throw new EntityNotFoundException("Publisher");
            }

            if (request.Genres != null)
            {
                comic.ComicGenres.Clear();
                foreach (var genre in request.Genres)
                {
                    var foundGenre = Context.Genres.FirstOrDefault(g => g.Id == genre);
                    if (foundGenre == null) throw new EntityNotFoundException("Genres");

                    comic.ComicGenres.Add(new ComicGenres
                    {
                        Comic = comic,
                        Genre = foundGenre,
                        UpdatedAt = DateTime.Now
                    });
                }
            }

            if (request.Authors != null)
            {
                comic.ComicAuthors.Clear();
                foreach (var author in request.Authors)
                {
                    var foundAuthor = Context.Authors.FirstOrDefault(a => a.Id == author);
                    if (foundAuthor == null) throw new EntityNotFoundException("Authors");

                    comic.ComicAuthors.Add(new ComicAuthors
                    {
                        Comic = comic,
                        Author = foundAuthor,
                        UpdatedAt = DateTime.Now
                    });
                }
            }

            Context.SaveChanges();
        }
    }
}
