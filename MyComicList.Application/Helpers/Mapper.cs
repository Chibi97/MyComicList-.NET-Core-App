using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;

namespace MyComicList.Application.Helpers
{
    public static class Mapper
    {
        public sealed class RenameTo : Attribute
        {
            public string DestinationName { get; set; }
        }

        public sealed class Skip : Attribute {}

        /**
         * Used to map source public properties to the destination public properties (DTO). 
         * You can use annotations for custom naming else the same name will be used.
         */
        public static void Automap(object source, object destination)
        {
            PropertyInfo[] sourceProperties = source.GetType().GetProperties();

            foreach(PropertyInfo property in sourceProperties)
            {
                Skip skip = property.GetCustomAttribute<Skip>();
                if (skip != null)
                    continue;

                RenameTo renamer = property.GetCustomAttribute<RenameTo>();
                if(renamer != null)
                {
                    SetPropertyValue(property, destination, source, renamer.DestinationName);
                } else
                {
                    SetPropertyValue(property, destination, source, property.Name);
                }
            }
        }

        private static void SetPropertyValue(PropertyInfo sourceProperty, object destination, object source, string name)
        {
            PropertyInfo destinationProperty = destination.GetType().GetProperty(name);
            object value = sourceProperty.GetValue(source);

            if (value == null)
                return;

            destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
        }
    }
}
