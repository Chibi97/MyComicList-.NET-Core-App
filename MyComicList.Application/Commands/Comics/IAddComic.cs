using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.Interfaces;

namespace MyComicList.Application.Commands.Comics
{
    public interface IAddComic : ICommand<ComicAddDTO>
    {
        
    }
}
