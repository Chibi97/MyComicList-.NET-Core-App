using System;
using System.Linq;
using MyComicList.Application.Commands.Publishers;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace MyComicList.EFCommands.Publishers
{
    public class EFDeletePublisher : EFBaseCommand, IDeletePublisher
    {
        public EFDeletePublisher(MyComicListContext context) : base(context)
        {

        }

        public void Execute(int id)
        {
            var publisher = Context.Publishers
                .Include(p => p.Comics)
                .Where(p => p.Id == id && p.DeletedAt == null)
                .FirstOrDefault();
            if (publisher == null) throw new EntityNotFoundException("Publisher", id);

            if (!publisher.Comics.Any())
            {
                publisher.Comics.Clear();
                publisher.DeletedAt = DateTime.Now;
                publisher.Name += "_Deleted_" + CurrentTimeStamp;
                Context.SaveChanges();

            }
            else throw new NotEmptyCollectionException("Publisher", "comics");
           
        }
    }
}
