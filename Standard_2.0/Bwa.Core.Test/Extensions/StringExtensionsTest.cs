using System.Text;
using Bwa.Core.Extensions;

namespace Bwa.Core.Test.Extensions;

public class StringExtensionsTest
{
	[Test]
	public void GetBytesWithDefaultEncoding()
	{
		Assert.AreEqual(Encoding.ASCII.GetBytes("blah"), "blah".GetBytes());
        Assert.AreEqual(Encoding.ASCII.GetBytes("foo"), "foo".GetBytes());
    }

    [Test]
    public void GetBytesWithAlternativeEncoding()
    {
        Assert.AreEqual(Encoding.UTF8.GetBytes("blah"), "blah".GetBytes(Encoding.UTF8));
        Assert.AreEqual(Encoding.BigEndianUnicode.GetBytes("foo"), "foo".GetBytes(Encoding.BigEndianUnicode));
    }

    [Test]
    public void YieldsCollectionOfRegexMatches()
    {
        Assert.IsTrue("".ReMatches(@"\d+").Empty());
        Assert.IsTrue("blah blah ...".ReMatches(@"\d+").Empty());
        var results = "1".ReMatches(@"\d+");
        Assert.AreEqual(1, results.Count());
        Assert.AreEqual("1", results.First());

        results = "foo bar foo baz".ReMatches(@"foo");
        Assert.AreEqual(2, results.Count());
        Assert.IsTrue(results.All(s => s == "foo"));
    }
}
