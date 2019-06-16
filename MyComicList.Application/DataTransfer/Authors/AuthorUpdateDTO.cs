using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
