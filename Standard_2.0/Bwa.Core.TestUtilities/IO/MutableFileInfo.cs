using Bwa.Core.IO;
using System;

namespace Bwa.Core.TestUtilities.IO
{
    public class MutableFileInfo : IFileInfo
    {
        public MutableFileInfo() { }
        public MutableFileInfo(string fullName) => FullName = fullName;
        public DateTime CreationTime { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastAccessTimeUtc { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }
        public IDirectoryInfo Directory { get; set; }
        public string DirectoryName { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public bool Exists { get; set; } = true;
        public bool IsReadOnly { get; set; }
        public long Length { get; set; }
        public string Contents { get; set; } = "";
        public string[] ReadAllLines() => Contents?.Split('\r', '\n') ?? Array.Empty<string>();
    }
}
