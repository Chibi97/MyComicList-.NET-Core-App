
using MyComicList.Application.DataTransfer.Authors;
using MyComicList.Application.Interfaces;

namespace MyComicList.Application.Commands.Authors
{
    public interface IUpdateAuthor : ICommand<AuthorUpdateDTO>
    {
        
    }
}
