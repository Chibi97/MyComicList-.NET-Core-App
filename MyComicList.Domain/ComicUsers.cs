using MyComicList.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyComicList.Domain
{
    public class ComicUsers : HistoryTracker
    {
        public int ComicId { get; set; }
        public Comic Comic { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
