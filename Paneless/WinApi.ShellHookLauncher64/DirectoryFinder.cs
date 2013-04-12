using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Paneless.WinApi
{
    public interface IDirectoryFinder
    {
        string FindDirectoryInAncestors(string fileName);
    }

    public class DirectoryFinder : IDirectoryFinder
    {
        private const string ChildDir = "lib";
        public string FindDirectoryInAncestors(string fileName)
        {
            var curDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (curDir.Parent != null)
            {
                if (IsFileInDir(fileName, curDir)) return curDir.FullName + fileName;

                if (IsFileInChildDir(fileName, curDir, ChildDir)) return curDir.FullName + "\\" + ChildDir + "\\" + fileName;

                curDir = curDir.Parent;
            }
            throw new FileNotFoundException("File not found", fileName);
        }

        private static bool IsFileInChildDir(string fileName, DirectoryInfo rootDir, string childDir)
        {
            string childFolderPath = rootDir.FullName + "\\" + ChildDir;
            if (Directory.Exists(childFolderPath))
            {
                return IsFileInDir(fileName, new DirectoryInfo(childFolderPath));
            }
            return false;
        }

        private static bool IsFileInDir(string fileName, DirectoryInfo rootDir)
        {
            IEnumerable<string> currentFolderFiles = Directory.EnumerateFiles(rootDir.FullName);
            return currentFolderFiles.Any(currentFile => currentFile.EndsWith(fileName));
        }
    }
}
