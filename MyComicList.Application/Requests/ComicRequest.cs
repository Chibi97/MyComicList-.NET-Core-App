using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static MyComicList.Application.CustomValidators.ListValidator;

namespace MyComicList.Application.Requests
{
    public class ComicRequest : PagedRequest
    {
        [MinLength(3, ErrorMessage = "Minimum number of characters is 3.")]
        [MaxLength(50, ErrorMessage = "Maximum number of characters is 50.")]
        public string Name { get; set; }

        [ListNotEmpty(ErrorMessage = "Collection of genres must contain at least one element.")]
        [UniqueIntegers(ErrorMessage = "Values for genres must be unique.")]
        public IEnumerable<int> Genres { get; set; }

        [ListNotEmpty(ErrorMessage = "Collection of authors must contain at least one element.")]
        [UniqueIntegers(ErrorMessage = "Values for authors must be unique.")]
        public IEnumerable<int> Authors { get; set; }
    }
}
