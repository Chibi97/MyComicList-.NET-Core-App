using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyComicList.Application.Helpers.Mapper;

namespace MyComicList.Application.DataTransfer.Users
{
    public class UserAddDTO
    {
        [Required, MinLength(2, ErrorMessage = "Minimum number of characters is 2.")]
        [MaxLength(30, ErrorMessage = "Maximum number of characters is 50.")]
        [RegularExpression(@"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$", ErrorMessage = "First name must begin with capital letters")]
        public string FirstName { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum number of characters is 2.")]
        [MaxLength(30, ErrorMessage = "Maximum number of characters is 50.")]
        [RegularExpression(@"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$", ErrorMessage = "Last name must begin with capital letters")]
        public string LastName { get; set; }

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
        [Skip]
        public string Password { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [MaxLength(50, ErrorMessage = "The email must be max 50 characters long.")]
        [MinLength(5, ErrorMessage = "Minimum number of characters is 5.")]
        [RegularExpression(@"^.+@.+$", ErrorMessage = "Email must contain @ symbol.")]
        public string Email { get; set; }

        [Skip, Required, Range(1, Int16.MaxValue)]
        public int Role { get; set; }
    }
}
