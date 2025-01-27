﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string property, int value) : base($"{property} - not valid. Given value: {value.ToString()} is not found.")
        {

        }

        public EntityNotFoundException(string property, string value) : base($"{property} - not valid. Given value: {value} is not found.")
        {

        }
    }
}
