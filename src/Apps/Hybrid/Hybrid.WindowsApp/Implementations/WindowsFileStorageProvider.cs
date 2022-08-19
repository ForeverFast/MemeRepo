using Domain.Core.Interfaces;
using System;
using System.IO;

namespace Hybrid.WindowsApp.Implementations
{
    internal class WindowsFileStorageProvider : IFileStorageProvider
    {
        public string RootPath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        public string TmpPath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "tmp");

        public string GetAbsolutePath(string path) => Path.Combine(RootPath, path);
        public string GetRelativePath(string path) => path.Replace(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "");


        public void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public string CreateFolderPath(string? parentFolderPath = null, string? title = null)
        {
            title ??= "Новая папка";
            parentFolderPath ??= Path.Combine(RootPath, "user-files");

            string newPath = Path.Combine(parentFolderPath, title);
            string tmpPath = newPath;
            int num = 2;

            do
            {
                if (!Directory.Exists(tmpPath))
                    return tmpPath;

                tmpPath = @$"{newPath}({num++})";
            }
            while (true);
        }

        public string CreateFilePath(string parentFolderPath, string extension, string? title = null)
        {
            title ??= "Новый мем";

            string newPath = Path.Combine(parentFolderPath, title);
            string tmpPath = newPath + extension;
            int num = 2;

            do
            {
                if (!File.Exists(tmpPath))
                    return tmpPath;

                tmpPath = @$"{newPath}({num++}){extension}";
            }
            while (true);
        }

        public void DeleteFolder(string path, bool recursive = true)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, recursive);
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }


        public string CopyFileToTmp(string sourceFile)
        {
            var targetTmpFolder = Path.Combine(TmpPath, Guid.NewGuid().ToString());
            this.CreateFolder(targetTmpFolder);
            var resultFileName = Path.GetFileName(sourceFile).Replace(" ", "");
            var newPath = Path.Combine(targetTmpFolder, resultFileName);
            File.Copy(sourceFile, newPath);
            return newPath;
        }

        public void CopyFileToNewFile(string sourceFile, string targetFile)
        {
            File.Copy(sourceFile, targetFile);
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
/*
   using var newFileFS = new FileStream(newPath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Delete);
            using var sourceFileFS = new FileStream(sourceFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            sourceFileFS.CopyTo(newFileFS);
 */