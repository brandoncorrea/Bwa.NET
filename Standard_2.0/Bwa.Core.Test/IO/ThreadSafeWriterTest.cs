using Bwa.Core.TestUtilities.IO;

namespace Bwa.Core.IO.Test;

[TestFixture]
public class ThreadSafeWriterTest
{
    [Test]
    public void InvokesMessagingAction()
    {
        var messages = new List<string>();
        var logger = new ThreadSafeWriter(messages.Add);
        logger.Write("blah");
        Assert.AreEqual(1, messages.Count);
        Assert.AreEqual("blah", messages[0]);
    }

    [Test]
    public void AddsNewlineCharacters()
    {
        var messages = new List<string>();
        var logger = new ThreadSafeWriter(messages.Add);
        logger.WriteLine("foo");
        logger.WriteLine("bar");
        Assert.AreEqual(2, messages.Count);
        Assert.AreEqual("foo\r\n", messages[0]);
        Assert.AreEqual("bar\r\n", messages[1]);
    }

    [Test]
    public void DefaultsToConsoleLogging()
    {
        var logger = new ThreadSafeWriter();
        var writer = new InMemoryTextWriter();
        Console.SetOut(writer);
        logger.WriteLine("foo");
        logger.Write("bar");
        Assert.AreEqual(2, writer.Messages.Count);
    }
}
