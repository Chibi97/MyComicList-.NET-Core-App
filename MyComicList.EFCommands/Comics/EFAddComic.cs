using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;
using MyComicList.Domain;

namespace MyComicList.EFCommands.Comics
{
    public class EFAddComic : EFBaseCommand, IAddComic
    {
        public EFAddComic(MyComicListContext context) : base(context)
        {
        }

        public void Execute(ComicDTO request)
        {
            if (Context.Comics.Any(c => c.Name == request.Name))
            {
                throw new EntityAlreadyExistsException(request.Name);
            };

            var publisher = Context.Publishers.FirstOrDefault(c => c.Name.ToLower().Equals(request.Publisher.ToLower()));
            if (publisher == null) throw new EntityNotFoundException("Publisher");
            var now = DateTime.Now;

            Comic newComic = new Comic
            {
                Name = request.Name,
                Description = request.Description,
                Issues = request.Issues,
                PublishedAt = request.PublishedAt,
                Publisher = publisher,
                CreatedAt = now,
                UpdatedAt = now
            };
            Context.Comics.Add(newComic);

            var genres = new List<ComicGenres>();
            var authors = new List<ComicAuthors>();

            foreach (var genre in request.Genres)
            {
                var foundGenre = Context.Genres.FirstOrDefault(g => g.Name.ToLower().Equals(genre.ToLower()));
                if (foundGenre == null) throw new EntityNotFoundException("Genres");
                
                var cg = new ComicGenres()
                {
                    Comic = newComic,
                    Genre = foundGenre,
                    CreatedAt = now,
                    UpdatedAt = now
                };
                genres.Add(cg);
            }
            newComic.ComicGenres = genres;

            foreach (var author in request.Authors)
            {
                var foundAuthor = Context.Authors.FirstOrDefault(g => g.FullName.ToLower().Equals(author.ToLower()));
                if (foundAuthor == null) throw new EntityNotFoundException("Authors");

                var ca = new ComicAuthors()
                {
                    Comic = newComic,
                    Author = foundAuthor,
                    CreatedAt = now,
                UpdatedAt = now
                };
                authors.Add(ca);
            }
            newComic.ComicAuthors = authors;

            try
            {
                Context.SaveChanges();

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    
}
