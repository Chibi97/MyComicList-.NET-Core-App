using MyComicList.Application.DataTransfer.Publishers;
using MyComicList.Application.Interfaces;

namespace MyComicList.Application.Commands.Publishers
{
    public interface IUpdatePublisher : ICommand<PublisherDTO>
    {
        
    }
}
