using System.Collections.Generic;
using System.IO;

namespace Bwa.Core.IO
{
    public interface IFileSystem
    {
        bool DirectoryExists(string path);
        bool DirectoryExists(DirectoryInfo directory);
        bool DirectoryExists(IDirectoryInfo directory);
        bool FileExists(string path);
        bool FileExists(FileInfo file);
        bool FileExists(IFileInfo file);
        IEnumerable<IFileInfo> GetFileInfos(string directory);
        IEnumerable<FileInfo> GetFileInfos(DirectoryInfo directory, string pattern);
        IEnumerable<IFileInfo> GetFileInfos(IDirectoryInfo directory, string pattern);
        string GetFileVersion(string path);
    }
}
