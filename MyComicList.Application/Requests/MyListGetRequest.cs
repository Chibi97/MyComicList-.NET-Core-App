using MyComicList.Domain;
using System.ComponentModel.DataAnnotations;

namespace MyComicList.Application.Requests
{
    public class MyListGetRequest : PagedRequest
    {
        public User User { get; set; }
        [MinLength(3, ErrorMessage = "Minimum number of characters is 3.")]
        [MaxLength(50, ErrorMessage = "Maximum number of characters is 50.")]
        public string ComicName { get; set; }

    }
}
