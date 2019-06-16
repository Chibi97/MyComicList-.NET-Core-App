using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Exceptions
{
    public class NotEmptyCollectionException : Exception
    {
        public NotEmptyCollectionException(string entity, string innerEntity) : base($"{entity} cannot be deleted because it has a collection of {innerEntity}")
        {

        }  
    }
}
