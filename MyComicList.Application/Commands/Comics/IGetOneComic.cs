﻿using MyComicList.Application.DataTransfer;

namespace MyComicList.Application.Commands.Comics
{
    public interface IGetOneComic : ICommand<int, ComicDTO>
    {
        
    }
}
