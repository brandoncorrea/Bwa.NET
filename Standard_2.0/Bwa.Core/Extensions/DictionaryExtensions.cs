using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bwa.Core.Extensions
{
	public static class DictionaryExtensions
	{
		public static T Deserialize<T>(this Dictionary<string, object> dictionary) =>
            (T)dictionary.Deserialize(typeof(T));

		public static object Deserialize(this Dictionary<string, object> dictionary, Type type) =>
			ReflectionHelper.Deserialize(dictionary.ContainsKey, dictionary.GetValue, type);

        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key) =>
			dictionary.ContainsKey(key) ? dictionary[key] : default;
	}
}
