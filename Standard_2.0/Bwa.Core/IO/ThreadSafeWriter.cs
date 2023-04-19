using System;

namespace Bwa.Core.IO
{
    public class ThreadSafeWriter : IWriter
    {
        private readonly object Lock = new object();
        protected readonly Action<string> WriteMessage;
        public ThreadSafeWriter() => WriteMessage = Console.Write;
        public ThreadSafeWriter(Action<string> writeMessage) => WriteMessage = writeMessage;
        public void WriteLine(string message) => Write($"{message}\r\n");
        public void WriteLine() => Write("\r\n");
        public void Write(string message)
        {
            lock (Lock)
            {
                WriteMessage(message);
            }
        }
    }
}
