using Domain.Core.Interfaces;
using System;
using System.IO;

namespace Hybrid.WindowsApp.Implementations
{
    internal class WindowsFileStorageProvider : IFileStorageProvider
    {
        public string RootPath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "user-files");

        public void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void DeleteFolder(string path, bool recursive = true)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, recursive);
        }

        public void CopyDirectoryTo(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        //public void CopyFileTo()

        #region Private methods 

        private void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            target = Directory.CreateDirectory(Path.Combine(target.FullName, source.Name));

            Array.ForEach(source.GetFiles(), fi => fi.CopyTo(Path.Combine(target.FullName, fi.Name), true));
            Array.ForEach(source.GetDirectories(), diSourceSubDir => CopyAll(diSourceSubDir, target));
        }

        #endregion
    }
}
