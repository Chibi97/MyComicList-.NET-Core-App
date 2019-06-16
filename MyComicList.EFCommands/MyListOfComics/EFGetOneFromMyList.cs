using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.MyList;
using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.DataTransfer.MyList;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Requests;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.MyListOfComics
{
    public class EFGetOneFromMyList : EFBaseCommand, IGetOneFromMyList
    {
        public EFGetOneFromMyList(MyComicListContext context) : base(context)
        {
        }

        public ComicGetDTO Execute(MyListDTO request)
        {
            var comic = Context.Comics
                        .Include(c => c.Publisher)
                        .Include(c => c.ComicGenres).ThenInclude(cg => cg.Genre)
                        .Include(c => c.ComicAuthors).ThenInclude(ca => ca.Author)
                        .Where(c => c.DeletedAt == null && c.Id == request.ComicId && c.Users.Any(cu => cu.ComicId == request.ComicId))
                        .SingleOrDefault();

            if (comic == null) throw new EntityNotFoundException("Comic", request.ComicId);
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
