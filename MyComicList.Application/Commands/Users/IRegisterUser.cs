using MyComicList.Application.DataTransfer.Auth;
using MyComicList.Application.Interfaces;

namespace MyComicList.Application.Commands.Users
{
    public interface IRegisterUser : ICommand<UserRegisterDTO>
    {

    }
}
