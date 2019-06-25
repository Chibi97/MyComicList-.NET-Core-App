using System;
using System.Collections.Generic;
using System.Text;

namespace MyComicList.Application.Exceptions
{
    public class UnsupportedTypeException : Exception
    {
        public UnsupportedTypeException(string property) : base($"Problem with: { property }. Given type is not supported.")
        { }
    }
}
