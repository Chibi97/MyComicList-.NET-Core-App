using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.DataTransfer.Genres
{
    public class GenreDTO
    {
        [Range(1,Int32.MaxValue)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
