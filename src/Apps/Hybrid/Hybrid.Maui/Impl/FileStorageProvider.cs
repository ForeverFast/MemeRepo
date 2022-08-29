using Domain.Core.Interfaces;

namespace Hybrid.Maui.Impl
{
    internal class FileStorageProvider : IFileStorageProvider
    {
        public string RootPath => throw new NotImplementedException();

        public string TmpPath => throw new NotImplementedException();

        public void CopyFolderTo(string sourceDirectory, string targetDirectory)
        {
            throw new NotImplementedException();
        }

        public void CopyFileToNewFile(string sourceFile, string targetFile)
        {
            throw new NotImplementedException();
        }

        public string CopyFileToTmp(string sourceFile)
        {
            throw new NotImplementedException();
        }

        public string CreateFilePath(string parentFolderPath, string extension, string title = null)
        {
            throw new NotImplementedException();
        }

        public void CreateFolder(string path)
        {
            throw new NotImplementedException();
        }

        public string CreateFolderPath(string parentFolderPath = null, string title = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string path)
        {
            throw new NotImplementedException();
        }

        public void DeleteFolder(string path, bool recursive = true)
        {
            throw new NotImplementedException();
        }

        public string GetAbsolutePath(string path)
        {
            throw new NotImplementedException();
        }

        public string GetRelativePath(string path)
        {
            throw new NotImplementedException();
        }

        public string OpenFilePicker(string extension = null)
        {
            throw new NotImplementedException();
        }

        public string OpenFolderPicker()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> OpenMultiselectPicker(bool folderPickerMode = false)
        {
            throw new NotImplementedException();
        }

        public void ShowFolder(string relativePath)
        {
            throw new NotImplementedException();
        }

        public void OpenFile(string relativePath)
        {
            throw new NotImplementedException();
        }

        public void CopyFileToClipboard(string relativePath)
        {
            throw new NotImplementedException();
        }
    }
}
