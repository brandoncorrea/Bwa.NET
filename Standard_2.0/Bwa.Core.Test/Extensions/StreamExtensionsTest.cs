using Bwa.Core.Extensions;

namespace Bwa.Core.Test.Extensions;

public class StreamExtensionsTest
{
    [Test]
    public void TailOfEmptyStream()
    {
        var lines = new string[] { "" };
        var stream = new MemoryStream();
        Assert.AreEqual(Array.Empty<string>(), stream.Tail(-1));
        Assert.AreEqual(Array.Empty<string>(), stream.Tail(0));
        Assert.AreEqual(lines, stream.Tail(1));
        Assert.AreEqual(lines, stream.Tail(2));
    }

    [Test]
    public void TailOfSingleLineStream()
    {
        var content = "Hello, world!";
        var lines = new string[] { content };
        var stream = new MemoryStream(content.GetBytes());
        Assert.AreEqual(lines, stream.Tail(1));
        Assert.AreEqual(lines, stream.Tail(10));
    }

    [Test]
    public void TailOfTwoLines()
    {
        var content = "Hello\nworld!";
        var stream = new MemoryStream(content.GetBytes());
        Assert.AreEqual(new string[] { "world!" }, stream.Tail(1));
        Assert.AreEqual(new string[] { "Hello", "world!" }, stream.Tail(2));
    }

    [Test]
    public void TailOfFiveLines()
    {
        var content = "Once\nupon a time\n\nthere was\na little dino!";
        var lines = content.Split('\n');
        var stream = new MemoryStream(content.GetBytes());
        Assert.AreEqual(lines.Skip(4), stream.Tail(1));
        Assert.AreEqual(lines.Skip(3), stream.Tail(2));
        Assert.AreEqual(lines.Skip(2), stream.Tail(3));
        Assert.AreEqual(lines.Skip(1), stream.Tail(4));
        Assert.AreEqual(lines, stream.Tail(5));
        Assert.AreEqual(lines, stream.Tail(6));
    }
}