using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.DataTransfer.Publishers
{
    public class PublisherDTO
    {
        [Range(1,Int32.MaxValue)]
        public int Id { get; set; }
        [MinLength(2, ErrorMessage = "Minimum number of characters is 2.")]
        [MaxLength(50, ErrorMessage = "Maximum number of characters is 50.")]
        public string Name { get; set; }
        [MinLength(2, ErrorMessage = "Minimum number of characters is 2.")]
        [MaxLength(50, ErrorMessage = "Maximum number of characters is 50.")]
        public string Origin { get; set; }
    }
}
