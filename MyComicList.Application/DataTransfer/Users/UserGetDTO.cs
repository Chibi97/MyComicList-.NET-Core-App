﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyComicList.Application.Helpers.Mapper;

namespace MyComicList.Application.DataTransfer.Users
{
    public class UserGetDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [Skip]
        public IEnumerable<string> Comics { get; set; }
    }
}