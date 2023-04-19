using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace Bwa.Core.IO
{
    public class ThreadSafeFileWriter : IWriter, IDisposable
    {
        public readonly string FilePath;

        private readonly ConcurrentQueue<string> Messages = new ConcurrentQueue<string>();
        private readonly StreamWriter Writer;
        private readonly Thread WorkerThread;
        private bool IsRunning;
        
        public ThreadSafeFileWriter(string filePath)
        {
            FilePath = filePath;
            Writer = new StreamWriter(FilePath, true);
            WorkerThread = new Thread(WriteLoop);
        }

        public virtual void WriteLine(string text) => Write($"{text}\r\n");
        public void WriteLine() => Write("\r\n");
        public void Write(string text) => Messages.Enqueue(text);
        public void Start()
        {
            WorkerThread.Start();
            IsRunning = true;
        }

        private void WriteLoop()
        {
            while (IsRunning)
            {
                FlushMessageQueue();
                Thread.Sleep(100);
            }
            FlushMessageQueue();
            Writer.Close();
        }

        private void FlushMessageQueue()
        {
            while (Messages.TryDequeue(out string message))
                Lambda.Try(() => Writer.Write(message));
            Writer.Flush();
        }

        public void Dispose() => IsRunning = false;
    }
}
