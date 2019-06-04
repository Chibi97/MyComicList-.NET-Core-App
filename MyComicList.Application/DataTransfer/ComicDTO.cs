using MyComicList.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyComicList.Application.DataTransfer
{
    public class ComicDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Issues { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Publisher { get; set; }
        //public ICollection<MyList> MyUsers { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<string> Authors { get; set; }
    }
}
