using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyComicList.Application.Commands.Publishers;
using MyComicList.Application.DataTransfer.Publishers;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Helpers;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.Publishers
{
    public class EFUpdatePublisher : EFBaseCommand, IUpdatePublisher
    {
        public EFUpdatePublisher(MyComicListContext context) : base(context)
        {
        }

        public void Execute(PublisherDTO request)
        {
            var publisher = Context.Publishers.Where(p => p.Id == request.Id && p.DeletedAt == null).FirstOrDefault();
            if (publisher == null) throw new EntityNotFoundException("Publisher", request.Id);

            if(publisher.Name != request.Name)
            {
                if(Context.Publishers.Where(p => p.DeletedAt == null).Any(p => p.Name == request.Name))
                {
                    throw new EntityAlreadyExistsException("Publisher", request.Name);
                }
            }

            Mapper.Automap(request, publisher);
            publisher.UpdatedAt = DateTime.Now;
            Context.SaveChanges();
        }
    }
}
