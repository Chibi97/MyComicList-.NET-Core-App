using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyComicList.Application.CustomValidators.ListValidator;

namespace MyComicList.Application.DataTransfer.Genres
{
    public class GenreAddDTO
    {
        [Required]
        public string Name { get; set; }

    }
}
