using MyComicList.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyComicList.Domain
{
    public class User : PrimaryKey
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<ComicUsers> ComicUsers { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
