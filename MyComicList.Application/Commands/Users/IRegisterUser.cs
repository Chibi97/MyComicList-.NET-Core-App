using MyComicList.Application.DataTransfer.Auth;
using MyComicList.Application.Interfaces;
using MyComicList.Application.Responses;

namespace MyComicList.Application.Commands.Users
{
    public interface IRegisterUser : ICommand<UserRegisterDTO, AuthorizedUserResponse>
    {

    }
}
