using MyComicList.Domain.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyComicList.Domain
{
    public class User : SoftDelete
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public ICollection<MyList> Comics { get; set; }

    }
}
