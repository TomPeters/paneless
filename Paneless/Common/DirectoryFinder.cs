using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Paneless.Common
{
    public static class DirectoryFinder
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string ChildDir = "lib";
        public static string FindDirectoryInAncestors(string fileName)
        {
            Logger.Debug("Looking for file: " + fileName);
            var curDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (curDir.Parent != null)
            {
                if (IsFileInDir(fileName, curDir)) return curDir.FullName + "\\" + fileName;

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
