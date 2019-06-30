using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyComicList.API.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool Verify(string givenPassword, string actualHash);
    }
}
