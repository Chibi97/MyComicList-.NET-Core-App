using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;
using MyComicList.Domain;

namespace MyComicList.EFCommands.MyListOfComics
{
    public class EFAddComic : EFBaseCommand, IAddComic
    {
        private List<ComicGenres> genres;
        private List<ComicAuthors> authors;
        private List<Picture> pictures;
        public EFAddComic(MyComicListContext context) : base(context)
        {
            genres = new List<ComicGenres>();
            authors = new List<ComicAuthors>();
            pictures = new List<Picture>();
        }

        public void Execute(ComicAddDTO request)
        {
            if (Context.Comics.Any(c => c.Name == request.Name))
            {
                throw new EntityAlreadyExistsException("Name", request.Name);
            };

            var publisher = Context.Publishers.FirstOrDefault(p => p.Id == request.Publisher && p.DeletedAt == null);
            if (publisher == null) throw new EntityNotFoundException("Publisher", request.Publisher);


            Comic newComic = new Comic
            {
                Name = request.Name.Trim(),
                Description = request.Description.Trim(),
                Issues = request.Issues,
                PublishedAt = request.PublishedAt,
                Publisher = publisher
            };
            pictures.Add(new Picture { Path = request.ImagePath });

            Context.Comics.Add(newComic);


            foreach (var genre in request.Genres)
            {
                var foundGenre = Context.Genres.FirstOrDefault(g => g.Id == genre && g.DeletedAt == null);
                if (foundGenre == null) throw new EntityNotFoundException("Genres", genre);

                var cg = new ComicGenres()
                {
                    Comic = newComic,
                    Genre = foundGenre
                };
                genres.Add(cg);

            }

            foreach (var author in request.Authors)
            {
                var foundAuthor = Context.Authors.FirstOrDefault(a => a.Id == author && a.DeletedAt == null);
                if (foundAuthor == null) throw new EntityNotFoundException("Authors", author);

                var ca = new ComicAuthors()
                {
                    Comic = newComic,
                    Author = foundAuthor
                };
                authors.Add(ca);
            }

            newComic.ComicGenres = genres;
            newComic.ComicAuthors = authors;
            newComic.Pictures = pictures;

            Context.SaveChanges();
        }

    }
}
