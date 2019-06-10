using MyComicList.Domain.Helpers;
using System.Collections.Generic;

namespace MyComicList.Domain
{
    public class User : SoftDelete
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<MyList> MyComics { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
