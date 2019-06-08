using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyComicList.Application.DataTransfer.Comics
{
    public class ComicGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Issues { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishedAt { get; set; }

        public string Publisher { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<string> Authors { get; set; }
    }
}
