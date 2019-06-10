using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.Helpers;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.Comics
{
    public class EFGetComics : EFBaseCommand, IGetComics
    {
        public EFGetComics(MyComicListContext context) : base(context) { }

        public PagedResponse<ComicGetDTO> Execute(ComicRequest request)
        {
            var comics = Context.Comics.AsQueryable();

            if (request.Genres != null)
            {
                comics = comics.Where(c => c.ComicGenres.Any(cg => request.Genres.Contains(cg.GenreId)));
            }

            if (request.Authors != null)
            {
                comics = comics.Where(c => c.ComicAuthors.Any(ca => request.Authors.Contains(ca.AuthorId)));
            }

            if (request.Name != null)
            {
                comics = comics.Where(c => c.Name.ToLower().Contains(request.Name.Trim().ToLower()));
            }

            return comics
                .Include(c => c.ComicGenres)
                .ThenInclude(cg => cg.Genre)
                .OrderBy(c => c.Id)
                .Where(c => c.DeletedAt == null)
                .Select(c => new ComicGetDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Issues = c.Issues,
                    PublishedAt = c.PublishedAt,
                    Publisher = c.Publisher.Name,
                    Genres = c.ComicGenres.Select(cg => cg.Genre.Name),
                    Authors = c.ComicAuthors.Select(ca => ca.Author.FullName)
                })
                .Paginate(request.PerPage, request.Page);
        }
    }
}
