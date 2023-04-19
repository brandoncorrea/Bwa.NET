using System;
using System.IO;

namespace Bwa.Core.IO
{
    public class DefaultFileInfo : IFileInfo
    {
        private readonly FileInfo BackingFile;
        public DefaultFileInfo(string file) => BackingFile = new FileInfo(file);
        public DefaultFileInfo(FileInfo file) => BackingFile = file;
        public static DefaultFileInfo Create(FileInfo file) => new DefaultFileInfo(file);
        public DateTime CreationTime => BackingFile.CreationTime;
        public DateTime CreationTimeUtc => BackingFile.CreationTimeUtc;
        public DateTime LastAccessTime => BackingFile.LastAccessTime;
        public DateTime LastAccessTimeUtc => BackingFile.LastAccessTimeUtc;
        public DateTime LastWriteTime => BackingFile.LastWriteTime;
        public DateTime LastWriteTimeUtc => BackingFile.LastWriteTimeUtc;
        public IDirectoryInfo Directory => new DefaultDirectoryInfo(BackingFile.Directory);
        public string DirectoryName => BackingFile.DirectoryName;
        public string FullName => BackingFile.FullName;
        public string Name => BackingFile.Name;
        public string Extension => BackingFile.Extension;
        public bool Exists => BackingFile.Exists;
        public bool IsReadOnly => BackingFile.IsReadOnly;
        public long Length => BackingFile.Length;
        public string[] ReadAllLines() => File.ReadAllLines(BackingFile.FullName);
    }
}
