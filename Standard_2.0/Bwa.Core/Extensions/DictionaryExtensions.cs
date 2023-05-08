using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bwa.Core.Extensions
{
	public static class DictionaryExtensions
	{
		public static T Deserialize<T>(this Dictionary<string, object> dictionary) =>
            (T)dictionary.Deserialize(typeof(T));

		public static object Deserialize(this Dictionary<string, object> dictionary, Type type)
		{
            var obj = Activator.CreateInstance(type);
            foreach (var prop in type.GetProperties())
            {
                if (dictionary.ContainsKey(prop.Name))
                {
                    var value = dictionary[prop.Name];
                    if (value != null)
                        prop.SetValue(obj, ResolvePropertyValue(prop, value));
                }
            }
            return obj;
        }

        private static object ResolvePropertyValue(PropertyInfo prop, object value)
        {
			var type = value.GetType();
            if (prop.PropertyType == type)
                return value;
            else if (prop.PropertyType.IsClass && typeof(Dictionary<string, object>) == type)
                return ((Dictionary<string, object>)value).Deserialize(prop.PropertyType);
            else
                return ((IConvertible)value).ToType(prop.PropertyType, null);
        }

        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key) =>
			dictionary.ContainsKey(key)
			? dictionary[key]
			: default;
	}
}
