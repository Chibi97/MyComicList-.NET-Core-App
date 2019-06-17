using MyComicList.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyComicList.Domain
{
    public class Comic : SoftDelete
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Issues { get; set; }
        public DateTime PublishedAt { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<MyList> Users { get; set; }
        public ICollection<ComicGenres> ComicGenres { get; set; }
        public ICollection<ComicAuthors> ComicAuthors { get; set; }

    }
}
