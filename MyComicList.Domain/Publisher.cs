using MyComicList.Domain.Helpers;
using System.Collections.Generic;

namespace MyComicList.Domain
{
    public class Publisher : SoftDelete
    {
        public string Name { get; set; }
        public ICollection<Comic> Comics { get; set; }
    }
}
