using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Bwa.Core.Extensions
{
    public static class StringExtensions
    {
        public static byte[] GetBytes(this string value) =>
            value.GetBytes(Encoding.ASCII);

        public static byte[] GetBytes(this string value, Encoding encoding) =>
            encoding.GetBytes(value);

        public static IEnumerable<string> ReMatches(this string input, string pattern)
        {
            foreach (Match match in Regex.Matches(input, pattern))
                yield return match.Value;
        }
    }
}