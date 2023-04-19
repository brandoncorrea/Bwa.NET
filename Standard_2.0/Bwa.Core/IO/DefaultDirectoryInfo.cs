using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bwa.Core.IO
{
    public class DefaultDirectoryInfo : IDirectoryInfo
    {
        private readonly DirectoryInfo Directory;
        public DefaultDirectoryInfo(string path) => Directory = new DirectoryInfo(path);
        public DefaultDirectoryInfo(DirectoryInfo directory) => Directory = directory;
        public DateTime CreationTime => Directory.CreationTime;
        public DateTime CreationTimeUtc => Directory.CreationTimeUtc;
        public DateTime LastAccessTime => Directory.LastAccessTime;
        public DateTime LastAccessTimeUtc => Directory.LastAccessTimeUtc;
        public DateTime LastWriteTime => Directory.LastWriteTime;
        public DateTime LastWriteTimeUtc => Directory.LastWriteTimeUtc;
        public IDirectoryInfo Root => new DefaultDirectoryInfo(Directory.Root);
        public IDirectoryInfo Parent => new DefaultDirectoryInfo(Directory.Parent);
        public string FullName => Directory.FullName;
        public string Name => Directory.Name;
        public string Extension => Directory.Extension;
        public bool Exists => Directory.Exists;
        public IEnumerable<IFileInfo> GetFiles() =>
            Directory.GetFiles().Select(DefaultFileInfo.Create);
        public IEnumerable<IFileInfo> GetFiles(string pattern) =>
            Directory.GetFiles(pattern).Select(DefaultFileInfo.Create);
    }
}
