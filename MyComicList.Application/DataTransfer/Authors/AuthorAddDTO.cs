
using System.ComponentModel.DataAnnotations;

namespace MyComicList.Application.DataTransfer.Authors
{
    public class AuthorAddDTO
    {
        [Required, MinLength(3, ErrorMessage = "Minimum number of characters is 3.")]
        [MaxLength(30, ErrorMessage = "Maximum number of characters is 30.")]
        public string FirstName { get; set; }
        [Required, MinLength(3, ErrorMessage = "Minimum number of characters is 3.")]
        [MaxLength(30, ErrorMessage = "Maximum number of characters is 30.")]
        public string LastName { get; set; }
    }
}
