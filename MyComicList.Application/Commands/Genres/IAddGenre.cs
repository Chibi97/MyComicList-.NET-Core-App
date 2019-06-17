using MyComicList.Application.DataTransfer.Genres;
using MyComicList.Application.Interfaces;

namespace MyComicList.Application.Commands.Genres
{
    public interface IAddGenre : ICommand<GenreAddDTO>
    {
        
    }
}
