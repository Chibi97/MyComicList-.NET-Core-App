using System;
using System.ComponentModel.DataAnnotations;
using static MyComicList.Application.Helpers.Mapper;

namespace MyComicList.Application.DataTransfer.Publishers
{
    public class PublisherDTO
    {
        [Skip]
        public int Id { get; set; }
        [MinLength(2, ErrorMessage = "Minimum number of characters is 2.")]
        [MaxLength(50, ErrorMessage = "Maximum number of characters is 50.")]
        public string Name { get; set; }
        [MinLength(2, ErrorMessage = "Minimum number of characters is 2.")]
        [MaxLength(50, ErrorMessage = "Maximum number of characters is 50.")]
        public string Origin { get; set; }
    }
}
