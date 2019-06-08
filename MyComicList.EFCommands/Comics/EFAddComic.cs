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
        private readonly List<ComicGenres> genres;
        private readonly List<ComicAuthors> authors;
        public EFAddComic(MyComicListContext context) : base(context)
        {
            genres = new List<ComicGenres>();
            authors = new List<ComicAuthors>();
        }

        public void Execute(ComicCreateDTO request)
        {
            if (Context.Comics.Any(c => c.Name == request.Name))
            {
                throw new EntityAlreadyExistsException("Name", request.Name);
            };

            var publisher = Context.Publishers.FirstOrDefault(p => p.Id == request.Publisher);
            if (publisher == null) throw new EntityNotFoundException("Publisher", request.Publisher.ToString());

            Comic newComic = new Comic
            {
                Name = request.Name,
                Description = request.Description,
                Issues = request.Issues,
                PublishedAt = request.PublishedAt,
                Publisher = publisher
            };
            Context.Comics.Add(newComic);

            foreach (var genre in request.Genres)
            {
                var foundGenre = Context.Genres.FirstOrDefault(g => g.Id == genre);
                if (foundGenre == null) throw new EntityNotFoundException("Genres");
                
                var cg = new ComicGenres()
                {
                    Comic = newComic,
                    Genre = foundGenre
                };
                genres.Add(cg);
            }
            newComic.ComicGenres = genres;

            foreach (var author in request.Authors)
            {
                var foundAuthor = Context.Authors.FirstOrDefault(a => a.Id == author);
                if (foundAuthor == null) throw new EntityNotFoundException("Authors");

                var ca = new ComicAuthors()
                {
                    Comic = newComic,
                    Author = foundAuthor
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
