﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException() : base("Entity with that name already exists.")
        { }

        public EntityAlreadyExistsException(object Property) : base($"Entity with name: { Property } already exists.")
        { }
    }
}