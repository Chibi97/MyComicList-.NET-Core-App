using MyComicList.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Domain
{
    public class ComicCategories : HistoryTracker
    {
        public int ComicId { get; set; }
        public Comic Comic { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
