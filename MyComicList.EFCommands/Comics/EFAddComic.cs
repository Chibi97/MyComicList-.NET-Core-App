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

            var publisher = Context.Publishers.FirstOrDefault(c => c.Name.ToLower().Contains(request.Publisher.ToLower()));

            Comic newComic = new Comic
            {
                Name = request.Name + new Random().NextDouble(),
                Description = request.Description,
                Issues = request.Issues,
                PublishedAt = request.PublishedAt,
                Publisher = publisher
            };
            Context.Comics.Add(newComic);

            var genres = new List<ComicGenres>();
            var authors = new List<ComicAuthors>();

            foreach (var genre in request.Genres)
            {
                var foundGenre = Context.Genres.FirstOrDefault(g => g.Name.ToLower().Contains(genre.ToLower()));
                var cg = new ComicGenres()
                {
                    Comic = newComic,
                    Genre = foundGenre
                };
                genres.Add(cg);
            }
            
            foreach (var author in request.Authors)
            {
                var foundAuthor = Context.Authors.FirstOrDefault(g => g.FullName.ToLower().Contains(author.ToLower()));
                var ca = new ComicAuthors()
                {
                    Comic = newComic,
                    Author = foundAuthor
                };
                authors.Add(ca);
            }

            newComic.ComicGenres = genres;
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
