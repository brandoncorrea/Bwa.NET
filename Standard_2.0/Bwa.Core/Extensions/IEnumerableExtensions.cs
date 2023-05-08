using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bwa.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source) =>
            source.Take(source.Count() - 1);

        public static string JoinString<T>(this IEnumerable<T> source, string separator) =>
            string.Join(separator, source);

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
            source.Where(i => !predicate(i));

        public static bool Empty<T>(this IEnumerable<T> source) => !source.Any();

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        public static async Task ForEachParallel<T>(this IEnumerable<T> collection, int threads, Action<T> action)
        {
            var queue = new ConcurrentQueue<T>(collection);
            await Lambda.InvokeParallel(Math.Min(threads, queue.Count), () =>
            {
                while (queue.TryDequeue(out T value))
                    action(value);
            });
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> action)
        {
            foreach (var item in source)
                await action(item);
        }
    }
}
