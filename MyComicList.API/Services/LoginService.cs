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
            var foundUser = context.Users.SingleOrDefault(u => u.Id == id);
            if(foundUser != null)
            {
                user = foundUser;
            }
        }
    }
}
