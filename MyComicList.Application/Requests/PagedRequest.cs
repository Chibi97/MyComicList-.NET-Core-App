using System;
using System.ComponentModel.DataAnnotations;

namespace MyComicList.Application.Requests
{
    public class PagedRequest
    {
        [Range(1, Int16.MaxValue, ErrorMessage = "At least one per page is allowed.")]
        public int PerPage { get; set; } = 10;
        [Range(1, Int16.MaxValue, ErrorMessage = "At least one page is allowed.")]
        public int Page { get; set; } = 1;
    }
}
