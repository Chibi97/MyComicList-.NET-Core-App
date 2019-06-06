using MyComicList.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyComicList.Application.DataTransfer
{
    public class ComicDTO
    {
        [Required, MinLength(3, ErrorMessage = "Minimum number of characters is 3.")]
        [MaxLength(50, ErrorMessage = "Maximum number of characters is 50.")]
        public string Name { get; set; }

        [Required, MinLength(10, ErrorMessage = "Minimum number of characters is 10.")]
        [MaxLength(700, ErrorMessage = "Maximum number of characters is 50.")]
        public string Description { get; set; }
        
        [Required]
        [Range(1, Int16.MaxValue, ErrorMessage = "Maximum number is 32767, and minimum is 1.")]
        public int Issues { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishedAt { get; set; }

        [Required]
        public string Publisher { get; set; }
        [Required]
        public IEnumerable<string> Genres { get; set; }
        [Required]
        public IEnumerable<string> Authors { get; set; }
        // TODO: obezbediti da kolekcije uvek budu prosledene kao niz
    }
}
