using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Commands.Users
{
    public interface IUpdateUser : ICommand<UserUpdateDTO>
    {
        
    }
}
