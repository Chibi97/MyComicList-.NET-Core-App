
using System.ComponentModel.DataAnnotations;

namespace MyComicList.Application.DataTransfer.Authors
{
    public class AuthorUpdateDTO
    {
        public int Id { get; set; }
        [MinLength(2, ErrorMessage = "Minimum number of characters is 2.")]
        [MaxLength(50, ErrorMessage = "Maximum number of characters is 30.")]
        public string FirstName { get; set; }
        [MinLength(2, ErrorMessage = "Minimum number of characters is 2.")]
        [MaxLength(50, ErrorMessage = "Maximum number of characters is 30.")]
        public string LastName { get; set; }
    }
}
