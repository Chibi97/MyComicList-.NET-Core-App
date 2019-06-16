
using System.ComponentModel.DataAnnotations;

namespace MyComicList.Application.DataTransfer.Genres
{
    public class GenreAddDTO
    {
        [Required, MinLength(2, ErrorMessage = "Minimum number of characters is 2.")]
        [MaxLength(50, ErrorMessage = "Maximum number of characters is 50.")]
        public string Name { get; set; }

    }
}
