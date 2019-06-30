using System;

namespace MyComicList.Shared.Services
{
    public interface ITokenService<DecryptFromToken, EncryptWithToken>
    {
        DecryptFromToken Decrypt(string token);
        string Encrypt(EncryptWithToken data);
    }
}
