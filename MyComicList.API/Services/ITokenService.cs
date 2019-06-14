using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.API.Services
{
    public interface ITokenService<DecryptFromToken, EncryptWithToken>
    {
        DecryptFromToken Decrypt(string token);
        string Encrypt(EncryptWithToken data);
    }
}
