using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bwa.Core.Extensions
{
	internal static class ReflectionHelper
	{
        public static object Deserialize(
            Func<string, bool> canSelect,
            Func<string, object> select,
            Type type)
        {
            var obj = Activator.CreateInstance(type);
            type.GetProperties()
                .Where(prop => canSelect(prop.Name))
                .ForEach(prop => prop.SetValue(obj, ResolveType(prop.PropertyType, select(prop.Name))));
            return obj;
        }

        public static object ResolveType(Type type, object value)
        {
            if (value == null || value.GetType() == typeof(DBNull))
                return null;
            var valueType = value.GetType();
            if (type == valueType)
                return value;
            else if (type.IsClass && typeof(Dictionary<string, object>) == valueType)
                return ((Dictionary<string, object>)value).Deserialize(type);
            else
                return ((IConvertible)value).ToType(type, null);
        }
    }
}

