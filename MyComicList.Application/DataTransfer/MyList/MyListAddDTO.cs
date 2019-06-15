using MyComicList.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static MyComicList.Application.CustomValidators.ListValidator;

namespace MyComicList.Application.DataTransfer.MyList
{
    public class MyListAddDTO
    {
        public User User { get; set; }
        [Required, ListNotEmpty(ErrorMessage = "Collection of comics must contain at least one element.")]
        [UniqueIntegers(ErrorMessage = "Values for comics must be unique.")]
        public IEnumerable<int> Comics { get; set; }
    }
}
