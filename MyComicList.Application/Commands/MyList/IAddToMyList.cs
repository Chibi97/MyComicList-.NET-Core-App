
using MyComicList.Application.DataTransfer.MyList;
using MyComicList.Application.Interfaces;

namespace MyComicList.Application.Commands.MyList
{
    public interface IAddToMyList : ICommand<MyListAddDTO>
    {
    }
}
