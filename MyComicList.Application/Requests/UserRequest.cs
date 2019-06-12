using System;

namespace MyComicList.Application.Requests
{
    public class UserRequest : PagedRequest
    {
        public string Username { get; set; }
    }
}
