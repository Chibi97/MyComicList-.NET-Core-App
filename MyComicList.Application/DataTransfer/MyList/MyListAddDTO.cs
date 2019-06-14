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
        public IEnumerable<int> Comics { get; set; }
    }
}
