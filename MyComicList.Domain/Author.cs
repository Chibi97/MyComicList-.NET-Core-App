using MyComicList.Domain.Helpers;
using System.Collections.Generic;

namespace MyComicList.Domain
{
    public class Author : SoftDelete
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public ICollection<ComicAuthors> ComicAuthors { get; set; }
    }
}
