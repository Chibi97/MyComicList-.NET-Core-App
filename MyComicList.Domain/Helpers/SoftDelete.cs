
using System;

namespace MyComicList.Domain.Helpers
{
    public class SoftDelete : PrimaryKey
    {
        public DateTime? DeletedAt { get; set; }
    }
}
