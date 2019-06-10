using MyComicList.Domain.Helpers;
using System.Collections.Generic;

namespace MyComicList.Domain
{
    public class Genre : SoftDelete
    {
        public string Name { get; set; }
        public ICollection<ComicGenres> ComicGenres { get; set; }
    }
}
