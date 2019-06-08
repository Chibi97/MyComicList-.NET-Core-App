using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Requests
{
    public class ComicRequest : PagedRequest
    {
        public int? GenreId { get; set; }
        public int? AuthorId { get; set; }
    }
}
