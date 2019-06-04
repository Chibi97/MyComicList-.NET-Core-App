using MyComicList.Domain.Helpers;

namespace MyComicList.Domain
{
    public class ComicGenres : HistoryTracker
    {
        public int ComicId { get; set; }
        public Comic Comic { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
