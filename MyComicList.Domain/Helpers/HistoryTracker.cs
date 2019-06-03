using System;
using System.Collections.Generic;
using System.Text;

namespace MyComicList.Domain.Helpers
{
    public class HistoryTracker : SoftDelete
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
