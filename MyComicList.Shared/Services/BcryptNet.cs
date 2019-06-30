using System;

namespace MyComicList.Shared.Services
{
    public class BcryptNet : IPasswordService
    {
        public string HashPassword(string passwordValue)
        {
            return BCrypt.Net.BCrypt.HashPassword(passwordValue);
        }

        public bool Verify(string passwordValue, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(passwordValue, hash);
        }
    }
}
