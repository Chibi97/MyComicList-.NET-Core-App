using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyComicList.Application.Commands.Publishers;
using MyComicList.Application.DataTransfer.Publishers;
using MyComicList.Application.Exceptions;
using MyComicList.DataAccess;
using MyComicList.Domain;

namespace MyComicList.EFCommands.Publishers
{
    public class EFAddPublisher : EFBaseCommand, IAddPublisher
    {
        public EFAddPublisher(MyComicListContext context) : base(context)
        {

        }

        public void Execute(PublisherAddDTO request)
        {       
            if (Context.Publishers.Where(p => p.DeletedAt == null).Any(p => p.Name == request.Name))
            {
                throw new EntityAlreadyExistsException("Publisher", request.Name);
            }

            Publisher newPublisher = new Publisher
            {
                Name = request.Name.Trim(),
                Origin = request.Origin.Trim(),
            };

            Context.Publishers.Add(newPublisher);
            Context.SaveChanges();
        }
    }
}
