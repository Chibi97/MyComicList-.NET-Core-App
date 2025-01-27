﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static MyComicList.Application.CustomValidators.ListValidator;

namespace MyComicList.Application.DataTransfer.Comics
{
    public class ComicAddDTO
    {
        [Required]
        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }

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
        [Range(1, Int16.MaxValue)]
        public int Publisher { get; set; }

        [Required, ListNotEmpty(ErrorMessage = "Collection of genres must contain at least one element.")]
        [UniqueIntegers(ErrorMessage = "Values for genres must be unique.")]
        public IEnumerable<int> Genres { get; set; }

        [Required, ListNotEmpty(ErrorMessage = "Collection of authors must contain at least one element.")]
        [UniqueIntegers(ErrorMessage = "Values for authors must be unique.")]
        public IEnumerable<int> Authors { get; set; }

    }
}
