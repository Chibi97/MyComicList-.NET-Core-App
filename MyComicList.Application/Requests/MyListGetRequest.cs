using MyComicList.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Requests
{
    public class MyListGetRequest : PagedRequest
    {
        public User User { get; set; }
        public string ComicName { get; set; }

    }
}
