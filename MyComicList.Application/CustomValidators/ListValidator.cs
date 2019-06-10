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
                return true;
            }
        }

        public sealed class UniqueIntegers : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                var collection = value as IEnumerable<int>;
                if(collection != null)
                {
                    return collection.Distinct().Count() == collection.Count();
                }
                
                return true;
            }
        }
    }

}
