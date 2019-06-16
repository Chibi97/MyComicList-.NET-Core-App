using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.DataTransfer.MyList;

namespace MyComicList.Application.Commands.MyList
{
    public interface IGetOneFromMyList : ICommand<MyListDTO, ComicGetDTO>
    {
        
    }
}
