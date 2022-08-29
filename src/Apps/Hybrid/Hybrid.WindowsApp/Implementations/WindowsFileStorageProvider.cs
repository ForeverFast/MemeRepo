using Domain.Core.Interfaces;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Hybrid.WindowsApp.Implementations
{
    internal class WindowsFileStorageProvider : IFileStorageProvider
    {
        public string RootPath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        public string TmpPath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "tmp");

        public string GetAbsolutePath(string path) => Path.Combine(RootPath, path);
        public string GetRelativePath(string path) => path.Replace(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "");

        #region Folder

        public void ShowFolder(string relativePath)
        {
            using (var proc = Process.Start("explorer", GetAbsolutePath(relativePath))) { }
        }

        public string OpenFolderPicker()
        {
            var dlg = new CommonOpenFileDialog();
            dlg.IsFolderPicker = true;
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                return dlg.FileName;
            else
                return string.Empty;
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

        public void CopyFolderTo(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);

            void CopyAll(DirectoryInfo source, DirectoryInfo target)
            {
                Directory.CreateDirectory(target.FullName);
                target = Directory.CreateDirectory(Path.Combine(target.FullName, source.Name));

                Array.ForEach(source.GetFiles(), fi => fi.CopyTo(Path.Combine(target.FullName, fi.Name), true));
                Array.ForEach(source.GetDirectories(), diSourceSubDir => CopyAll(diSourceSubDir, target));
            }
        }

        #endregion

        #region File

        public void OpenFile(string relativePath)
        {
            using (Process p = new Process())
            {
                p.StartInfo = new ProcessStartInfo("explorer", GetAbsolutePath(relativePath)); /*{ UseShellExecute = true }*/
                p.Start();
            }
        }

        public string OpenFilePicker(string? extension = null)
        {
            extension ??= "*.jpg;*.png";

            var dlg = new CommonOpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filters.Add(new CommonFileDialogFilter("Файлы", extension));
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                return dlg.FileName;
            else
                return string.Empty;
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

        public void CopyFileToClipboard(string relativePath)
        {
            Clipboard.SetImage(new BitmapImage(new Uri(GetAbsolutePath(relativePath))));
        }

        #endregion

        public IEnumerable<string> OpenMultiselectPicker(bool folderPickerMode = false)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Multiselect = true;
            dlg.IsFolderPicker = folderPickerMode;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                return dlg.FileNames;
            else
                return Enumerable.Empty<string>();
        }
    }
}
