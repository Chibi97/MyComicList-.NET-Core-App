using MyComicList.Domain.Helpers;

namespace MyComicList.Domain
{
    public class Review : PrimaryKey
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ComicId { get; set; }
        public Comic Comic { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
