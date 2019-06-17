using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.DataTransfer.MyList;
using MyComicList.Application.Interfaces;

namespace MyComicList.Application.Commands.MyList
{
    public interface IGetOneFromMyList : ICommand<MyListDTO, ComicGetDTO>
    {
        
    }
}
