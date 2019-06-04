using MyComicList.Domain.Helpers;

namespace MyComicList.Domain
{
    public class MyList : HistoryTracker
    {
        public int ComicId { get; set; }
        public Comic Comic { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
