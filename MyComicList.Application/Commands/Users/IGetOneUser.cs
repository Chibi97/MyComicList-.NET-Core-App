using MyComicList.Application.DataTransfer.Users;

namespace MyComicList.Application.Commands.Users
{
    public interface IGetOneUser : ICommand<int, UserGetDTO>
    { }
}
