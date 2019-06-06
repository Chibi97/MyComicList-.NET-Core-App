using MyComicList.Application.DataTransfer;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;
using System.Collections.Generic;

namespace MyComicList.Application.Commands.Comics
{
    public interface IGetComics : ICommand<ComicRequest, IEnumerable<ComicDTO>>
    {
    }
}
