using System;

namespace Bwa.Core.IO
{
    public interface ILogger : IWriter
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Exception(Exception ex);
    }
}
