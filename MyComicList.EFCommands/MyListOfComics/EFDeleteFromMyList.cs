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

            var comic = Context.Comics.Where(c => c.DeletedAt == null && c.Id == request.ComicId).FirstOrDefault();
            if (comic == null) throw new EntityNotFoundException("Comic", request.ComicId);

            var oneSet = new MyList()
            {
                Comic = comic,
                User = request.User
            };
            var user = Context.Users.Include(u => u.Comics).Where(u => u.Id == request.User.Id).ToList();
            
            // TODO: obrisati zapravo

            Context.SaveChanges();
        }
    }
}
