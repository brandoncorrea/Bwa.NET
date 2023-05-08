using System;

namespace Bwa.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Last(this DateTime date, DayOfWeek day)
        {
            int mod = (int)day - (int)date.DayOfWeek;
            if (date.DayOfWeek <= day) mod -= 7;
            return date.AddDays(mod).Date;
        }

        public static DateTime EndOfDay(this DateTime date) =>
            date.Date.AddDays(1).AddTicks(-1);

        public static bool Between(this DateTime date, DateTime before, DateTime after) =>
            before < date && date < after;
    }
}
