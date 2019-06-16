using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static MyComicList.Application.CustomValidators.ListValidator;
using static MyComicList.Application.Helpers.Mapper;

namespace MyComicList.Application.DataTransfer.Comics
{
    public class ComicUpdateDTO
    {
        [Skip]
        public int ComicId { get; set; }

        [MinLength(3, ErrorMessage = "Minimum number of characters is 3.")]
        [MaxLength(50, ErrorMessage = "Maximum number of characters is 50.")]

        [Skip]
        public string Name { get; set; }

        [MinLength(10, ErrorMessage = "Minimum number of characters is 10.")]
        [MaxLength(700, ErrorMessage = "Maximum number of characters is 50.")]
        public string Description { get; set; }

        [Range(1, Int16.MaxValue, ErrorMessage = "Maximum number is 32767, and minimum is 1.")]
        public int? Issues { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PublishedAt { get; set; }

        [Skip]
        public int? Publisher { get; set; }

        [ListNotEmpty(ErrorMessage = "Collection of genres must contain at least one element.")]
        [UniqueIntegers(ErrorMessage = "Values for genres must be unique.")]
        [Skip]
        public IEnumerable<int> Genres { get; set; }

       [ListNotEmpty(ErrorMessage = "Collection of authors must contain at least one element.")]
       [UniqueIntegers(ErrorMessage = "Values for authors must be unique.")]
       [Skip]
        public IEnumerable<int> Authors { get; set; }
    }
}
