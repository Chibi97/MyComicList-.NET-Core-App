﻿
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static MyComicList.Application.CustomValidators.ListValidator;

namespace MyComicList.Application.Requests
{
    public class MyListAddRequest
    {
        [Required, ListNotEmpty(ErrorMessage = "Collection of comics must contain at least one element.")]
        [UniqueIntegers(ErrorMessage = "Values for comics must be unique.")]
        public IEnumerable<int> Comics { get; set; }
    }
}
