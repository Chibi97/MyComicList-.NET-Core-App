using MyComicList.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace MyComicList.Domain
{
    public class Comic : PrimaryKey
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Issues { get; set; }
        public DateTime PublishedAt { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<MyList> MyUsers { get; set; }
        public ICollection<ComicGenres> ComicGenres { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ComicAuthors> ComicAuthors { get; set; }

    }
}
