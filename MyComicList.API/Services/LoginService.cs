using Microsoft.EntityFrameworkCore;
using MyComicList.Application.DataTransfer;
using MyComicList.DataAccess;
using MyComicList.Domain;
using System;
using System.Linq;

namespace MyComicList.API.Services
{
    public class LoginService : ILoginService
    {
        private User user;
        private readonly MyComicListContext context;

        public LoginService(MyComicListContext context)
        {
            this.context = context;
        }

        public User PossibleUser()
        {
            return user;
        }

        public void Login(int id)
        {
            var foundUser = context.Users.Include(u => u.Role).SingleOrDefault(u => u.Id == id && u.DeletedAt == null);

            if(foundUser != null)
            {
                user = foundUser;
            }
            
        }
    }
}
