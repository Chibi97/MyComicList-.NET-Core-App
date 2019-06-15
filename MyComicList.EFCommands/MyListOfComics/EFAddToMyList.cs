using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyComicList.Application.Commands.MyList;
using MyComicList.Application.DataTransfer.MyList;
using MyComicList.Application.Exceptions;
using MyComicList.Domain;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.MyListOfComics
{
    public class EFAddToMyList : EFBaseCommand, IAddToMyList
    {
        public EFAddToMyList(MyComicListContext context) : base(context) { }

        public void Execute(MyListAddDTO request)
        {
            if(request.User.DeletedAt != null)
            {
                throw new EntityNotFoundException("User", request.User.Id);
            }

            foreach (var comicId in request.Comics)
            {
                var comic = Context.Comics.Where(c => c.DeletedAt == null && c.Id == comicId).FirstOrDefault();
                if (comic == null) throw new EntityNotFoundException("Comic", comicId);

                var user = Context.Users.Include(u => u.Comics).Where(u => u.Id == request.User.Id).ToList();

                var alreadyExists = user.Any(u => u.Comics.Any(uc => uc.ComicId == comicId));
                if (alreadyExists) throw new EntityAlreadyExistsException("Comic for your user", comicId);

                var oneSet = new MyList()
                {
                    Comic = comic,
                    User = request.User
                };

                request.User.Comics.Add(oneSet);
            }

            Context.SaveChanges();
        }

    }
}
