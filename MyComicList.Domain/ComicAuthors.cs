using MyComicList.Domain.Helpers;

namespace MyComicList.Domain
{
    public class ComicAuthors : HistoryTracker
    {
        public int ComicId { get; set; }
        public Comic Comic { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
