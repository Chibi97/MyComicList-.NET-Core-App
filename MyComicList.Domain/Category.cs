using MyComicList.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyComicList.Domain
{
    public class Category : PrimaryKey
    {
        public string Name { get; set; }
        public ICollection<ComicCategories> ComicCategories { get; set; }
    }
}
