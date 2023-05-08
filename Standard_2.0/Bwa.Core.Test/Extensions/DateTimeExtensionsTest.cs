using Bwa.Core.Extensions;

namespace Bwa.Core.Test.Extensions;

[TestFixture]
public class DateTimeExtensionsTest
{
    [Test]
    public void DateForLastDayOfWeek()
    {
        var from = new DateTime(2022, 3, 4, 1, 2, 3);
        Assert.AreEqual(new DateTime(2022, 2, 26), from.Last(DayOfWeek.Saturday));
        Assert.AreEqual(new DateTime(2022, 2, 27), from.Last(DayOfWeek.Sunday));
        Assert.AreEqual(new DateTime(2022, 2, 28), from.Last(DayOfWeek.Monday));
        Assert.AreEqual(new DateTime(2022, 3, 1), from.Last(DayOfWeek.Tuesday));
        Assert.AreEqual(new DateTime(2022, 3, 2), from.Last(DayOfWeek.Wednesday));
        Assert.AreEqual(new DateTime(2022, 3, 3), from.Last(DayOfWeek.Thursday));
        Assert.AreEqual(new DateTime(2022, 2, 25), from.Last(DayOfWeek.Friday));
        
        from = new DateTime(2022, 3, 3, 1, 2, 3);
        Assert.AreEqual(new DateTime(2022, 2, 26), from.Last(DayOfWeek.Saturday));
        Assert.AreEqual(new DateTime(2022, 2, 27), from.Last(DayOfWeek.Sunday));
        Assert.AreEqual(new DateTime(2022, 2, 28), from.Last(DayOfWeek.Monday));
        Assert.AreEqual(new DateTime(2022, 3, 1), from.Last(DayOfWeek.Tuesday));
        Assert.AreEqual(new DateTime(2022, 3, 2), from.Last(DayOfWeek.Wednesday));
        Assert.AreEqual(new DateTime(2022, 2, 24), from.Last(DayOfWeek.Thursday));
        Assert.AreEqual(new DateTime(2022, 2, 25), from.Last(DayOfWeek.Friday));
    }

    [Test]
    public void EndOfTheCurrentDay()
    {
        var date = new DateTime(2022, 3, 4, 1, 2, 3, 4);
        var end = date.Date.Add(new TimeSpan(1, 0, 0, 0)).AddTicks(-1);
        Assert.AreEqual(end, date.EndOfDay());
    }

    [Test]
    public void DatesBetween()
    {
        var d1 = new DateTime(2023, 1, 1);
        var d2 = new DateTime(2023, 1, 2);
        var d3 = new DateTime(2023, 1, 3);
        Assert.IsTrue(d2.Between(d1, d3));
        Assert.IsFalse(d2.Between(d3, d1));
        Assert.IsFalse(d2.Between(d2, d3));
        Assert.IsFalse(d2.Between(d1, d2));
        Assert.IsFalse(d1.Between(d1, d1));
        Assert.IsFalse(d1.Between(d2, d3));
        Assert.IsFalse(d3.Between(d1, d2));
    }
}
