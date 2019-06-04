using MyComicList.Domain.Helpers;
using System.Collections.Generic;

namespace MyComicList.Domain
{
    public class Publisher : PrimaryKey
    {
        public string Name { get; set; }
        public ICollection<Comic> Comics { get; set; }
    }
}
