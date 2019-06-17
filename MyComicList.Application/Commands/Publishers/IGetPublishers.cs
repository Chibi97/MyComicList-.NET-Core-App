
using MyComicList.Application.DataTransfer.Publishers;
using MyComicList.Application.Interfaces;
using MyComicList.Application.Requests;
using System.Collections.Generic;

namespace MyComicList.Application.Commands.Publishers
{
    public interface IGetPublishers : ICommand<PublisherRequest, IEnumerable<PublisherDTO>>
    {
        
    }
}
