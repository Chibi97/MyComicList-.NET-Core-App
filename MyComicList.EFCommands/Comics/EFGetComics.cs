using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;
using MyComicList.DataAccess;

namespace MyComicList.EFCommands.Comics
{
    public class EFGetComics : EFBaseCommand, IGetComics
    {
        public EFGetComics(MyComicListContext context) : base(context) { }
        
        public PagedResponse<ComicDTO> Execute(ComicRequest request)
        {
            
            return new PagedResponse<ComicDTO>(); // TODO: izmeniti
        }

        PagedResponse<ComicDTO> ICommand<ComicRequest, PagedResponse<ComicDTO>>.Execute(ComicRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
