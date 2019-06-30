
using System.ComponentModel.DataAnnotations;

namespace MyComicList.Application.DataTransfer.Auth
{
    public class UserRegisterDTO : UserLoginDTO
    {
        [Required, MinLength(2, ErrorMessage = "Minimum number of characters is 2.")]
        [MaxLength(30, ErrorMessage = "Maximum number of characters is 50.")]
        [RegularExpression(@"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$", ErrorMessage = "First name must begin with capital letters")]
        public string FirstName { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum number of characters is 2.")]
        [MaxLength(30, ErrorMessage = "Maximum number of characters is 50.")]
        [RegularExpression(@"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$", ErrorMessage = "Last name must begin with capital letters")]
        public string LastName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [MaxLength(50, ErrorMessage = "The email must be max 50 characters long.")]
        [MinLength(5, ErrorMessage = "Minimum number of characters is 5.")]
        [RegularExpression(@"^.+@.+$", ErrorMessage = "Email must contain @ symbol.")]
        public string Email { get; set; }
    }
}
