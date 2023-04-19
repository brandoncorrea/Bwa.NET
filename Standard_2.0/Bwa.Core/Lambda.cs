using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Bwa.Core
{
    public static class Lambda
    {
        public static void Times(this int times, Action action)
        {
            while (times-- > 0) action();
        }

        public static void Times(this int times, Action<int> action)
        {
            while (times > 0) action(times--);
        }

        public static IEnumerable<T> Times<T>(this int times, Func<T> f) => times.Times(_ => f());

        public static IEnumerable<T> Times<T>(this int times, Func<int, T> f)
        {
            for (var i = 0; i < times; i++)
                yield return f(i);
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> collection, Func<T, Task> action)
        {
            foreach (var item in collection)
                await action(item);
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action(item);
        }

        public static IEnumerable<string> ReMatches(string input, string pattern)
        {
            foreach(Match match in Regex.Matches(input, pattern))
                yield return match.Value;
        }

        public static bool Empty<T>(this IEnumerable<T> collection) => !collection.Any();

        public static string JoinString<T>(this IEnumerable<T> collection, string separator) =>
            string.Join(separator, collection);

        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, Func<T, bool> predicate) =>
            collection.Where(i => !predicate(i));

        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source) =>
            source.Take(source.Count() - 1);

        public static void Try(Action action)
        {
            try { action(); } catch { }
        }

        public static async Task InvokeParallel(int threads, Action action) =>
            await Task.WhenAll(threads.Times(() => Task.Run(action)));

        public static async Task ForEachParallel<T>(this IEnumerable<T> collection, int threads, Action<T> action)
        {
            var queue = new ConcurrentQueue<T>(collection);
            await InvokeParallel(Math.Min(threads, queue.Count), () =>
            {
                while (queue.TryDequeue(out T value))
                    action(value);
            });
        }

        public static T DoTo<T>(this T seed, params Func<T, T>[] operations) =>
            operations.Aggregate(seed, (v, f) => f(v));

        
        public static bool WaitFor<T>(this Func<T> f, T value, int waitMs = 0, int retries = -1) =>
            WaitFor(() => { }, () => value.Equals(f()), waitMs, retries);

        public static bool WaitFor<T>(this Func<T> f, Func<bool> pred, int waitMs = 0, int retries = -1) =>
            WaitFor(() => { f(); }, pred, waitMs, retries);

        public static bool WaitFor(this Action f, Func<bool> pred, int waitMs = 0, int retries = -1) =>
            WaitForAsync(() =>
            {
                f();
                return Task.CompletedTask;
            }, pred, waitMs, retries)
            .GetAwaiter()
            .GetResult();

        public static async Task<bool> WaitForAsync<T>(this Func<Task<T>> f, T value, int waitMs = 0, int retries = -1)
        {
            T result = default;
            return await WaitForAsync(
                async () => result = await f(),
                () => value.Equals(result),
                waitMs,
                retries);
        }

        public static Task<bool> WaitForAsync<T>(this Func<Task<T>> f, Func<bool> pred, int waitMs = 0, int retries = -1) =>
            WaitForAsync(async () => { await f(); }, pred, waitMs, retries);

        public static async Task<bool> WaitForAsync(this Func<Task> f, Func<bool> pred, int waitMs = 0, int retries = -1)
        {
            while (true)
            {
                await f();
                if (pred()) return true;
                if (retries != -1 && --retries < 0) return false;
                Thread.Sleep(waitMs);
            }
        }
    }
}
