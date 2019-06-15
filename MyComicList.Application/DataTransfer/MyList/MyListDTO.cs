using MyComicList.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.DataTransfer.MyList
{
    public class MyListDTO
    {
        public int ComicId { get; set; }
        public User User { get; set; }
    }
}
