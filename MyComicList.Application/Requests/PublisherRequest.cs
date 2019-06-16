using System.ComponentModel.DataAnnotations;

namespace MyComicList.Application.Requests
{
    public class PublisherRequest
    {
        [MinLength(3, ErrorMessage = "Minimum number of characters is 3.")]
        [MaxLength(50, ErrorMessage = "Maximum number of characters is 50.")]
        public string Origin { get; set; }
    }
}
