using MyComicList.Domain.Helpers;
using System.ComponentModel.DataAnnotations;

namespace MyComicList.Domain
{
    public class Picture : PrimaryKey
    {
        public string Path { get; set; }
        public int ComicId { get; set; }
        public Comic Comic { get; set; }
    }
}
