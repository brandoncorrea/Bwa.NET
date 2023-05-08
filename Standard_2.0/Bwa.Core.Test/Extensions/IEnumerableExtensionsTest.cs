using System;
using Bwa.Core.Extensions;

namespace Bwa.Core.Test.Extensions;

public class IEnumerableExtensionsTest
{
    [Test]
    public void ForEach()
    {
        var sum = 0;
        new[] { 1, 2, 3 }.ForEach(i => sum += i);
        Assert.AreEqual(6, sum);
    }

    [Test]
    public async Task AsynchronousForEach()
    {
        Task action(int _) => Task.CompletedTask;
        await new[] { 1, 2, 3 }.ForEachAsync(action);
    }

    [Test]
    public void ChecksForEmptyCollections()
    {
        Assert.IsTrue(Enumerable.Empty<int>().Empty());
        Assert.IsFalse(new string[] { "hello" }.Empty());
        Assert.IsFalse(new int[] { 1, 4 }.Empty());
    }

    [Test]
    public void JoinsCollectionsToStrings()
    {
        Assert.AreEqual("a\nb\nc", new[] { 'a', 'b', 'c' }.JoinString("\n"));
        Assert.AreEqual("1 fish, 2 fish, 3", new[] { 1, 2, 3 }.JoinString(" fish, "));
    }

    [Test]
    public void WhereComplement()
    {
        var strings = new[] { "", "hello", "\r\n\t ", " W\ro\nr\tl d ", "    " };
        var results = strings.Except(string.IsNullOrWhiteSpace);
        Assert.AreEqual(2, results.Count());
        Assert.AreEqual("hello", results.First());
        Assert.AreEqual(" W\ro\nr\tl d ", results.Last());
    }

    [Test]
    public async Task InvokesMultithreadedActionOnCollection()
    {
        var value = 0;
        var start = DateTime.Now;
        await Enumerable.Range(1, 4).ForEachParallel(4, i =>
        {
            value += i;
            Thread.Sleep(50);
        });
        var end = DateTime.Now;
        Assert.AreEqual(10, value);
        Assert.IsTrue(new TimeSpan(1000000) > end - start);
    }
}

