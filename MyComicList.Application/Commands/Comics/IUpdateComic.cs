﻿using MyComicList.Application.DataTransfer.Comics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Commands.Comics
{
    public interface IUpdateComic : ICommand<ComicUpdateDTO>
    {
        
    }
}