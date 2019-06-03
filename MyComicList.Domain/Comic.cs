using MyComicList.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyComicList.Domain
{
    public class Comic : PrimaryKey
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Issues { get; set; }
        public ICollection<ComicUsers> ComicUsers { get; set; }
        public ICollection<ComicCategories> ComicCategories { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
