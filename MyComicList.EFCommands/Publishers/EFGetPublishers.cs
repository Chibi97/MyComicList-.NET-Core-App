using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyComicList.Application.Commands.Publishers;
using MyComicList.Application.DataTransfer.Publishers;
using MyComicList.Application.Requests;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.Publishers
{
    public class EFGetPublishers : EFBaseCommand, IGetPublishers
    {
        public EFGetPublishers(MyComicListContext context) : base(context)
        {
        }

        public IEnumerable<PublisherDTO> Execute(PublisherRequest request)
        {
            var publisher = Context.Publishers.AsQueryable();

            if(request.Origin != null)
            {
                publisher = publisher.Where(p => p.Origin.ToLower().Contains(request.Origin.Trim().ToLower()));
            }

            return publisher
                   .Where(p => p.DeletedAt == null)
                   .OrderBy(p => p.Id)
                   .Select(p => new PublisherDTO
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Origin = p.Origin
                   });
        }
    }
}
