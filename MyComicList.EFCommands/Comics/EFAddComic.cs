using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;
using MyComicList.Domain;

namespace MyComicList.EFCommands.Comics
{
    public class EFAddComic : EFBaseCommand, IAddComic
    {
        public EFAddComic(MyComicListContext context) : base(context) { }

        public void Execute(ComicCreateDTO request)
        {
            if (Context.Comics.Any(c => c.Name == request.Name))
            {
                throw new EntityAlreadyExistsException("Name", request.Name);
            };

            var publisher = Context.Publishers.FirstOrDefault(p => p.Id == request.Publisher);
            if (publisher == null) throw new EntityNotFoundException("Publisher", request.Publisher);

            Comic newComic = new Comic
            {
                Name = request.Name.Trim(),
                Description = request.Description.Trim(),
                Issues = request.Issues,
                PublishedAt = request.PublishedAt,
                Publisher = publisher
            };
            Context.Comics.Add(newComic);

            MakeGenres(request, newComic);
            
            MakeAuthors(request, newComic);

            Context.SaveChanges();
        }

        #region Private Methods
        private void MakeGenres(ComicCreateDTO request, Comic newComic)
        {
            foreach (var genre in request.Genres)
            {
                var foundGenre = Context.Genres.FirstOrDefault(g => g.Id == genre);
                if (foundGenre == null) throw new EntityNotFoundException("Genres", genre);

                newComic.ComicGenres.Add(new ComicGenres()
                {
                    Comic = newComic,
                    Genre = foundGenre
                });
            }
        }

        private void MakeAuthors(ComicCreateDTO request, Comic newComic)
        {
            foreach (var author in request.Authors)
            {
                var foundAuthor = Context.Authors.FirstOrDefault(a => a.Id == author);
                if (foundAuthor == null) throw new EntityNotFoundException("Authors", author);

                newComic.ComicAuthors.Add(new ComicAuthors()
                {
                    Comic = newComic,
                    Author = foundAuthor
                });
            }
        }

        #endregion
    }
}
