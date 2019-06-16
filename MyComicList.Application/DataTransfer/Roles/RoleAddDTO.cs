

using System.ComponentModel.DataAnnotations;

namespace MyComicList.Application.DataTransfer.Roles
{
    public class RoleAddDTO
    {
        [Required, MinLength(3, ErrorMessage = "Minimum number of characters is 3.")]
        [MaxLength(20, ErrorMessage = "Maximum number of characters is 20.")]
        public string Name { get; set; }
    }
}
