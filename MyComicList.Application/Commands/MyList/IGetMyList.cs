using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;

namespace MyComicList.Application.Commands.MyList
{
    public interface IGetMyList : ICommand<MyListGetRequest, PagedResponse<ComicGetDTO>>
    {
        
    }
}
