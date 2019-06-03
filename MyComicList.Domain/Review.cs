using MyComicList.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
