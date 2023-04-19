using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Bwa.Core.TestUtilities.IO
{
    public class InMemoryTextWriter : TextWriter
    {
        public List<string> Messages = new List<string>();
        public override Encoding Encoding => Encoding.Default;

        public InMemoryTextWriter()
            : base(CultureInfo.InvariantCulture)
        {
        }

        public override void Write(char[] buffer, int index, int count) =>
            throw new NotImplementedException();

        public override void Write(string value) => Messages.Add(value);
        public override void WriteLine() => Write("\r\n");
        public override void WriteLine(string value) => Write($"{value}\r\n");
        public override void WriteLine(object value) => Write($"{value}\r\n");
    }
}
