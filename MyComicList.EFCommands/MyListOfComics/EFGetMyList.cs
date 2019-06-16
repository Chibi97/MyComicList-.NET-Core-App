using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.MyList;
using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.Helpers;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.MyListOfComics
{
    public class EFGetMyList : EFBaseCommand, IGetMyList
    {
        public EFGetMyList(MyComicListContext context) : base(context)
        {
        }

        public PagedResponse<ComicGetDTO> Execute(MyListGetRequest request)
        {
            var comics = Context.Comics.AsQueryable();
            if (request.ComicName != null)
            {
                comics = comics.Where(c => c.Name.ToLower().Contains(request.ComicName.Trim().ToLower()));
            }

            return comics
                .Include(c => c.ComicGenres)
                .ThenInclude(cg => cg.Genre)
                .Include(c => c.Users)
                .ThenInclude(cu => cu.User)
                .OrderBy(c => c.Id)
                .Where(c => c.DeletedAt == null && c.Users.Any(u => u.UserId == request.User.Id))
                .Select(c => new ComicGetDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Issues = c.Issues,
                    PublishedAt = c.PublishedAt,
                    Publisher = c.Publisher.Name,
                    Genres = c.ComicGenres.Select(cg => cg.Genre.Name),
                    Authors = c.ComicAuthors.Select(ca => ca.Author.FirstName + ' ' + ca.Author.LastName)
                })
                .Paginate(request.PerPage, request.Page);
        }
    }
}
