using MyComicList.Application.Commands.Comics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Commands.Users
{
    public interface IDeleteUser : ICommand<int>
    {
        
    }
}
