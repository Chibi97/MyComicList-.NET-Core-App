using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.Comics
{
    public class EFGetComics : EFBaseCommand, IGetComics
    {
        public EFGetComics(MyComicListContext context) : base(context) { }

        public IEnumerable<ComicGetDTO> Execute(ComicRequest request)
        {
            // PagedResponse<ComicDTO> ispraviti

            var comics = Context.Comics.AsQueryable();
            return comics
                .Include(c => c.ComicGenres)
                .ThenInclude(cg => cg.Genre)
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
                });
        }
    }
}
