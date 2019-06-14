using MyComicList.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Domain
{
    public class Role : SoftDelete
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
