using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Commands.MyList
{
    public interface IGetMyList : ICommand<MyListGetRequest, PagedResponse<ComicGetDTO>>
    {
        
    }
}
