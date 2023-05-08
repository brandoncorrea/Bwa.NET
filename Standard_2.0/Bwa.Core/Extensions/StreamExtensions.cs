using System;
using System.IO;
using System.Text;

namespace Bwa.Core.Extensions
{
    public static class StreamExtensions
    {
        public static string[] Tail(this Stream stream, int lines)
        {
            if (lines <= 0) return Array.Empty<string>();
            byte[] buffer = Encoding.UTF8.GetBytes("\n");
            int sizeOfChar = buffer.Length;
            var tokenCount = 0;
            var endPosition = stream.Length / sizeOfChar;

            for (var position = sizeOfChar; position < endPosition; position += sizeOfChar)
            {
                stream.Seek(-position, SeekOrigin.End);
                stream.Read(buffer, 0, buffer.Length);

                if (Encoding.UTF8.GetString(buffer) == "\n" &&
                    ++tokenCount == lines)
                {
                    byte[] returnBuffer = new byte[stream.Length - stream.Position];
                    stream.Read(returnBuffer, 0, returnBuffer.Length);
                    return Encoding.UTF8.GetString(returnBuffer).Split('\n');
                }
            }

            // handle case where number of tokens in file is less than numberOfTokens
            stream.Seek(0, SeekOrigin.Begin);
            buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer).Split('\n');
        }
    }
}