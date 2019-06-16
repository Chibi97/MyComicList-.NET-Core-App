using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.MyList;
using MyComicList.Application.DataTransfer.MyList;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;
using MyComicList.Domain;

namespace MyComicList.EFCommands.MyListOfComics
{
    public class EFDeleteFromMyList : EFBaseCommand, IDeleteFromMyList
    {
        public EFDeleteFromMyList(MyComicListContext context) : base(context)
        {
        }
        public void Execute(MyListDTO request)
        {
            if (request.User.DeletedAt != null)
            {
                throw new EntityNotFoundException("User", request.User.Id);
            }

            Context.Entry(request.User)
                .Collection(u => u.Comics)
                .Load();

            var comic = request.User.Comics.FirstOrDefault(c => c.ComicId == request.ComicId);
             if (comic == null) throw new EntityNotFoundException("Comic", request.ComicId);

            request.User.Comics.Remove(comic);
            Context.SaveChanges();
        }
    }
}
