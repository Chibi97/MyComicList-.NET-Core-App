
using System.ComponentModel.DataAnnotations;

namespace MyComicList.Application.DataTransfer.Auth
{
    public class UserLoginDTO
    {
        [Required, MaxLength(20, ErrorMessage = "The username must be max 20 characters long.")]
        [MinLength(3, ErrorMessage = "Minimum number of characters is 3.")]
        [RegularExpression(@"^[A-Za-z0-9]+(?:[ _-][A-Za-z0-9]+)*$",
            ErrorMessage = "Username must contain lowe or upper case letters and digits. Only _ and - allowed, but not one after another, or at the start and at the end.")]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "The password must be max 50 characters long.")]
        [MinLength(8, ErrorMessage = "Minimum number of characters is 8.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one of these: @ $ ! % * ? & special characters.")]
        public string Password { get; set; }
    }
}
