using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.Application.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException() : base(message: "Token is not valid.")
        { }
    }

}
