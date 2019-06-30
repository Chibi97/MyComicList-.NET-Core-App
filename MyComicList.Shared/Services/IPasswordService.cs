using System;

namespace MyComicList.Shared.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool Verify(string givenPassword, string actualHash);
    }
}
