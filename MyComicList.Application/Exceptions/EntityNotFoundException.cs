using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base("Entity you are searching for is not found.")
        {

        }

        public EntityNotFoundException(string property) : base($"{property} - not valid. Check if exist, or insert before this action")
        {

        }
    }
}
