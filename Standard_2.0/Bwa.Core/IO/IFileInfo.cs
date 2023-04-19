using System;

namespace Bwa.Core.IO
{
    public interface IFileInfo
    {
        DateTime CreationTime { get; }
        DateTime CreationTimeUtc { get; }
        DateTime LastAccessTime { get; }
        DateTime LastAccessTimeUtc { get; }
        DateTime LastWriteTime { get; }
        DateTime LastWriteTimeUtc { get; }
        IDirectoryInfo Directory { get; }
        string DirectoryName { get; }
        string FullName { get; }
        string Name { get; }
        string Extension { get; }
        bool Exists { get; }
        bool IsReadOnly { get; }
        long Length { get; }
        string[] ReadAllLines();
    }
}
