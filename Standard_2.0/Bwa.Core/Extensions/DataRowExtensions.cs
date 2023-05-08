using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Bwa.Core.Extensions
{
	public static class DataRowExtensions
	{
        public static T Deserialize<T>(this DataRow row) =>
            (T)row.Deserialize(typeof(T));

        public static object Deserialize(this DataRow row, Type type) =>
            ReflectionHelper.Deserialize(row.ContainsKey, row.GetValue, type);

        public static object GetValue(this DataRow row, string key) =>
            row.ContainsKey(key) ? row[key] : default;

        public static bool ContainsKey(this DataRow row, string key) =>
            row.Table.Columns.Contains(key);
    }
}

