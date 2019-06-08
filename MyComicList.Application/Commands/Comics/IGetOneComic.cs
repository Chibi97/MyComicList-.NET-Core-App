using MyComicList.Application.DataTransfer.Comics;

namespace MyComicList.Application.Commands.Comics
{
    public interface IGetOneComic : ICommand<int, ComicGetDTO>
    {
        
    }
}
