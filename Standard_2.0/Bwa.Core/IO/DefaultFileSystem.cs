using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Bwa.Core.IO
{
    public class DefaultFileSystem : IFileSystem
    {
        private static IFileSystem _instance;
        public static IFileSystem Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DefaultFileSystem();
                return _instance;
            }
        }

        public bool DirectoryExists(string path) => Directory.Exists(path);
        public bool DirectoryExists(DirectoryInfo directory) => directory.Exists;
        public bool DirectoryExists(IDirectoryInfo directory) => directory.Exists;
        public bool FileExists(string path) => File.Exists(path);
        public bool FileExists(FileInfo file) => file.Exists;
        public bool FileExists(IFileInfo file) => file.Exists;
        public IEnumerable<FileInfo> GetFileInfos(DirectoryInfo directory, string pattern) => directory.GetFiles(pattern);
        public IEnumerable<IFileInfo> GetFileInfos(IDirectoryInfo directory, string pattern) => directory.GetFiles(pattern);
        public IEnumerable<IFileInfo> GetFileInfos(string directory) =>
            new DefaultDirectoryInfo(directory).GetFiles();
        public string GetFileVersion(string path) => FileVersionInfo.GetVersionInfo(path).FileVersion;
    }
}
