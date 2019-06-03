using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyComicList.Application.Commands.Comics
{
    public interface IGetComics : ICommand<ComicRequest, PagedResponse<ComicDTO>>
    {
    }
}
