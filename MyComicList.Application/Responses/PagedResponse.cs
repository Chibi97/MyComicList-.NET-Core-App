﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Responses
{
    public class PagedResponse<T>
    {
        public int CurrentPage { get; set; }
        public int PagesCount { get; set; }
        public int PerPage { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
