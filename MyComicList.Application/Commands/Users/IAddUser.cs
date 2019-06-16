using MyComicList.Application.DataTransfer.Users;

namespace MyComicList.Application.Commands.Users
{
    public interface IAddUser : ICommand<UserCreateDTO>
    {
        
    }
}
