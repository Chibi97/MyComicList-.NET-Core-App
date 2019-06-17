using MyComicList.Application.DataTransfer.Users;
using MyComicList.Application.Interfaces;

namespace MyComicList.Application.Commands.Users
{
    public interface IGetOneUser : ICommand<int, UserGetDTO>
    { }
}
