using System;
using System.Collections.Generic;

namespace Bwa.Core.IO
{
    public interface IDirectoryInfo
    {
        DateTime CreationTime { get; }
        DateTime CreationTimeUtc { get; }
        DateTime LastAccessTime { get; }
        DateTime LastAccessTimeUtc { get; }
        DateTime LastWriteTime { get; }
        DateTime LastWriteTimeUtc { get; }
        IDirectoryInfo Root { get; }
        IDirectoryInfo Parent { get; }
        string FullName { get; }
        string Name { get; }
        string Extension { get; }
        bool Exists { get; }
        IEnumerable<IFileInfo> GetFiles();
        IEnumerable<IFileInfo> GetFiles(string pattern);
    }
}
