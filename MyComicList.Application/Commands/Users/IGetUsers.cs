using MyComicList.Application.DataTransfer.Users;
using MyComicList.Application.Interfaces;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;

namespace MyComicList.Application.Commands.Users
{
    public interface IGetUsers : ICommand<UserRequest, PagedResponse<UserGetDTO>>
    {
        
    }
}
