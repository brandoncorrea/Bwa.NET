using Bwa.Core.IO;
using System;
using System.Collections.Generic;

namespace Bwa.Core.TestUtilities.IO
{
    public class InMemoryLogger : ILogger
    {
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Messages { get; set; } = new List<string>();
        public List<string> Infos { get; set; } = new List<string>();
        public List<string> Warnings { get; set; } = new List<string>();
        public void Error(string message) => Errors.Add(message);
        public void Exception(Exception ex) => Error(ex.Message);
        public void Info(string message) => Infos.Add(message);
        public void Warn(string message) => Warnings.Add(message);
        public void Write(string text) => Messages.Add(text);
        public void WriteLine(string text) => Write($"{text}\r\n");
        public void WriteLine() => Write("\r\n");
    }
}
