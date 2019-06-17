using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.Interfaces;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;

namespace MyComicList.Application.Commands.Comics
{
    public interface IGetComics : ICommand<ComicRequest, PagedResponse<ComicGetDTO>>
    {
    }
}
