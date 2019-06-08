using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string value) : base($"Instance named: { value } already exists.")
        { }

        public EntityAlreadyExistsException(string property, string value) : base($"{ property } with value: { value } already exists.")
        { }
    }
}
