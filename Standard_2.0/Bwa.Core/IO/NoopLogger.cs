using System;

namespace Bwa.Core.IO
{
    public class NoopLogger : ILogger
    {
        private static ILogger _instance;
        public static ILogger Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NoopLogger();
                return _instance;
            }
        }

        public void Error(string message) { }

        public void Exception(Exception ex) { }
        public void Info(string message) { }
        public void Warn(string message) { }
        public void Write(string text) { }
        public void WriteLine(string text) { }
        public void WriteLine() { }
    }
}
