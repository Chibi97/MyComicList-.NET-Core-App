﻿using System.ComponentModel.DataAnnotations;

namespace MyComicList.Application.Requests
{
    public class UserRequest : PagedRequest
    {
        [MinLength(3, ErrorMessage = "Minimum number of characters is 3.")]
        [MaxLength(20, ErrorMessage = "Maximum number of characters is 20.")]
        public string Username { get; set; }
    }
}
