using Bwa.Core.IO;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bwa.Core.TestUtilities.IO
{
    public class InMemoryFileSystem : IFileSystem
    {
        public IEnumerable<IDirectoryInfo> Directories { get; set; } = Enumerable.Empty<IDirectoryInfo>();
        public IEnumerable<IFileInfo> Files { get; set; } = Enumerable.Empty<IFileInfo>();
        public bool DirectoryExists(string path) => Directories.FirstOrDefault(x => x.FullName == path)?.Exists ?? false;
        public bool DirectoryExists(DirectoryInfo directory) => DirectoryExists(directory.FullName);
        public bool DirectoryExists(IDirectoryInfo directory) => DirectoryExists(directory.FullName);
        public bool FileExists(string path) => Files.FirstOrDefault(x => x.FullName == path)?.Exists ?? false;
        public bool FileExists(IFileInfo file) => FileExists(file.FullName);
        public bool FileExists(FileInfo file) => FileExists(file.FullName);
        public IEnumerable<FileInfo> GetFileInfos(DirectoryInfo directory, string pattern) =>
            Files
            .Where(f => Regex.IsMatch(f.FullName, "^" + pattern.Replace(@"\", @"\\").Replace(".", @"\.").Replace(@"*", @".*")))
            .Select(f => new FileInfo(f.FullName));

        public IEnumerable<IFileInfo> GetFileInfos(IDirectoryInfo directory, string pattern)
        {
            var rePattern = "^" + pattern
                .Replace(@"\", @"\\")
                .Replace(".", @"\.")
                .Replace(@"*", @".*");

            return Files.Where(f => Regex.IsMatch(f.FullName, rePattern));
        }

        public IEnumerable<IFileInfo> GetFileInfos(string directory) =>
            Directories.FirstOrDefault(i => i.FullName == directory)?.GetFiles()
            ?? throw new DirectoryNotFoundException();

        public string GetFileVersion(string path) =>
            FileExists(path)
            ? "1.2.3"
            : throw new FileNotFoundException();
    }
}
