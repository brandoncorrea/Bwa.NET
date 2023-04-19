using System;

namespace Bwa.Core.IO
{
    public class ThreadSafeFileLogger : ThreadSafeFileWriter, ILogger
    {
        public ThreadSafeFileLogger(string path) : base(path)
        {
        }

        public override void WriteLine(string text) => base.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {text}");
        public void Error(string message) => WriteLine($"ERROR | {message}");
        public void Info(string message) => WriteLine($"INFO | {message}");
        public void Warn(string message) => WriteLine($"WARN | {message}");

        public void Exception(Exception ex)
        {
            Error(ex.Message);
            Error(ex.StackTrace);
        }
    }
}
