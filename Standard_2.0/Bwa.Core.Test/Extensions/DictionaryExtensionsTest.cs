using System;
using System.Collections.Generic;
using Bwa.Core.Extensions;

namespace Bwa.Core.Test.Extensions;

public class DictionaryExtensionsTest
{
    [Test]
    public void DeserializesEmptyDictionary()
    {
        var result = new Dictionary<string, object>().Deserialize<Author>();
        Assert.AreEqual('\0', result.FirstInitial);
        Assert.AreEqual(null, result.LastName);
        Assert.AreEqual(DateTime.MinValue, result.DateOfBirth);
        Assert.AreEqual(0, result.Age);
        Assert.IsFalse(result.IsPublished);
        Assert.AreEqual(0, result.NetWorth);
    }

    [Test]
    public void DeserializesPopulatedDictionary()
    {
        var result = new Dictionary<string, object>
        {
            { "FirstInitial", 'B' },
            { "LastName", "Junior" },
            { "DateOfBirth", new DateTime(1989, 3, 4) },
            { "Age", 22 },
            { "NetWorth", 246.8 },
            { "IsPublished", true }
        }.Deserialize<Author>();
        Assert.AreEqual('B', result.FirstInitial);
        Assert.AreEqual("Junior", result.LastName);
        Assert.AreEqual(new DateTime(1989, 3, 4), result.DateOfBirth);
        Assert.AreEqual(22, result.Age);
        Assert.IsTrue(result.IsPublished);
        Assert.AreEqual(246.8, result.NetWorth);
    }

    [Test]
    public void CastsFromStringToTypes()
    {
        var dict = new Dictionary<string, object>
        {
            { "FirstInitial", "R" },
            { "LastName", "McDonald" },
            { "Age", "22" },
            { "DateOfBirth", "1989-03-04" },
            { "IsPublished", "True" },
            { "NetWorth", "123.4567" }
        };
        var result = dict.Deserialize<Author>();
        Assert.AreEqual('R', result.FirstInitial);
        Assert.AreEqual("McDonald", result.LastName);
        Assert.AreEqual(22, result.Age);
        Assert.AreEqual(new DateTime(1989, 3, 4), result.DateOfBirth);
        Assert.IsTrue(result.IsPublished);
        Assert.AreEqual(123.4567, result.NetWorth);
    }

    [Test]
    public void IgnoresNullVaues()
    {
        var dict = new Dictionary<string, object>
        {
            { "FirstInitial", null },
            { "LastName", null },
            { "Age", null },
            { "DateOfBirth", null },
            { "IsPublished", null },
            { "NetWorth", null }
        };
        var result = dict.Deserialize<Author>();
        Assert.AreEqual('\0', result.FirstInitial);
        Assert.IsNull(result.LastName);
        Assert.AreEqual(0, result.Age);
        Assert.AreEqual(DateTime.MinValue, result.DateOfBirth);
        Assert.IsFalse(result.IsPublished);
        Assert.AreEqual(0, result.NetWorth);
    }

    [Test]
    public void ResolvesClassObjectReferences()
    {
        var author = new Author
        {
            LastName = "Roberts",
            Age = 34,
        };
        var dict = new Dictionary<string, object>
        {
            { "Author", author },
            { "Title", "The book" },
        };
        var book = dict.Deserialize<Book>();
        Assert.AreEqual(author, book.Author);
        Assert.AreEqual("The book", book.Title);
    }

    [Test]
    public void ResolvesClassObjectByDictionary()
    {
        var author = new Dictionary<string, object>
        {
            { "LastName", "Roberts" },
            { "Age", 34 },
        };
        var dict = new Dictionary<string, object>
        {
            { "Author", author },
            { "Title", "The book" },
        };
        var book = dict.Deserialize<Book>();
        Assert.AreEqual("Roberts", book.Author.LastName);
        Assert.AreEqual(34, book.Author.Age);
        Assert.AreEqual("The book", book.Title);
    }
}

public class Book
{
    public Author Author { get; set; }
    public string Title { get; set; }
}

public class Author
{
    public char FirstInitial { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public double NetWorth { get; set; }
    public bool IsPublished { get; set; }
    public DateTime DateOfBirth { get; set; }
}
