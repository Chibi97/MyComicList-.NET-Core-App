using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyComicList.Application.DataTransfer
{
    public class ComicDTO
    {
        [MinLength(3, ErrorMessage = "Mora tri slova bar")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Issues { get; set; }
    }
}
