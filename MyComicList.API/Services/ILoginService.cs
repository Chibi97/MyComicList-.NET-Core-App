using MyComicList.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.API.Services
{
    public interface ILoginService
    {
        User PossibleUser();
        void Login(int id);
    }
}
