using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyComicList.Application.CustomValidators
{
    public class ListValidator
    {
        public sealed class ListNotEmpty : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                var collection = value as IEnumerable;
                if (collection != null)
                {
                    return collection.GetEnumerator().MoveNext();
                }
                return false;
            }
        }

        public sealed class UniqueIntegers : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                var collection = value as IEnumerable<int>;
                if(collection.Distinct().Count() == collection.Count())
                {
                    return true;
                }
                
                return false;
            }
        }
    }

}
