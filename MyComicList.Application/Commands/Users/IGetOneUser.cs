using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer.Users;
using MyComicList.Application.Requests;

namespace MyComicList.Application.Commands.Users
{
    public interface IGetOneUser : ICommand<int, UserGetDTO>
    { }
}
