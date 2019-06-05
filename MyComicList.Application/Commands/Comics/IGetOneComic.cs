using MyComicList.Application.DataTransfer;
using MyComicList.Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyComicList.Application.Commands.Comics
{
    public interface IGetOneComic : ICommand<int, ComicDTO>
    {
        
    }
}
