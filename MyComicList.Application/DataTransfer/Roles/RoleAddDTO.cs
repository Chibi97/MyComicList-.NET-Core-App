using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.DataTransfer.Roles
{
    public class RoleAddDTO
    {
        [Required, MinLength(3, ErrorMessage = "Minimum number of characters is 3.")]
        [MaxLength(20, ErrorMessage = "Maximum number of characters is 20.")]
        public string Name { get; set; }
    }
}
