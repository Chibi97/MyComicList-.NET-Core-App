using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer.MyList;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyComicList.Application.Commands.MyList
{
    public interface IAddToMyList : ICommand<MyListAddDTO>
    {
    }
}
