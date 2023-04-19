using Bwa.Core.IO;
using System;
using System.Collections.Concurrent;

namespace Bwa.Core.TestUtilities.IO
{
    public class ThreadSafeInMemoryLogger : ILogger
    {
        public ConcurrentQueue<string> Errors { get; set; } = new ConcurrentQueue<string>();
        public ConcurrentQueue<string> Infos { get; set; } = new ConcurrentQueue<string>();
        public ConcurrentQueue<string> Warnings { get; set; } = new ConcurrentQueue<string>();
        public ConcurrentQueue<string> Messages { get; set; } = new ConcurrentQueue<string>();
        public void Error(string message) => Errors.Enqueue($"{message}\r\n");
        public void Exception(Exception ex) => Error(ex.Message);
        public void Info(string message) => Infos.Enqueue(message);
        public void Warn(string message) => Warnings.Enqueue(message);
        public void Write(string text) => Messages.Enqueue(text);
        public void WriteLine(string text) => Write($"{text}\r\n");
        public void WriteLine() => Write("\r\n");
    }
}
