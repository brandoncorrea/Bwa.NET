using Bwa.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bwa.Core.TestUtilities.IO
{
    public class MutableDirectoryInfo : IDirectoryInfo
    {
        public IEnumerable<IFileInfo> Files { get; set; } = Enumerable.Empty<IFileInfo>();
        public MutableDirectoryInfo() { }
        public MutableDirectoryInfo(string fullName) => FullName = fullName;
        public DateTime CreationTime { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastAccessTimeUtc { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }
        public IDirectoryInfo Root { get; set; }
        public IDirectoryInfo Parent { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public bool Exists { get; set; }
        public IEnumerable<IFileInfo> GetFiles() => Files;
        public IEnumerable<IFileInfo> GetFiles(string pattern)
        {
            var rePattern = "^" + pattern
                .Replace(@"\", @"\\")
                .Replace(".", @"\.")
                .Replace(@"*", @".*");

            return Files.Where(f => Regex.IsMatch(f.FullName, rePattern));
        }
    }
}
