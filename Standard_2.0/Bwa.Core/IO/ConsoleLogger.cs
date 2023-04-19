using System;

namespace Bwa.Core.IO
{
    public class ConsoleLogger : ILogger
    {
        private static ILogger _instance { get; set; }
        public static ILogger Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConsoleLogger();
                return _instance;
            }
        }

        public void Write(string text) => Console.Write(text);
        public void WriteLine(string text) => Console.WriteLine(text);
        public void WriteLine() => Console.WriteLine();

        public void Exception(Exception ex)
        {
            Error(ex.Message);
            Error(ex.StackTrace);
        }

        public void Error(string message) => Console.Error.WriteLine($"ERROR | {message}");
        public void Info(string message) => WriteLine($"INFO | {message}");
        public void Warn(string message) => WriteLine($"WARN | {message}");
    }
}
