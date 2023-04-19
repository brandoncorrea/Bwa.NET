using System.IO;

namespace Bwa.Core.IO
{
    public class FileWriter : IWriter
    {
        private readonly StreamWriter Writer;
        public FileWriter(string path) => Writer = new StreamWriter(path, true)
        {
            AutoFlush = true
        };
        public void WriteLine(string text) => Write($"{text}\r\n");
        public void WriteLine() => Write("\r\n");
        public void Write(string text) => Writer.Write(text);
    }
}
