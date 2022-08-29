namespace Domain.Core.Interfaces
{
    public interface IFileStorageProvider
    {
        string RootPath { get; }
        string TmpPath { get; }

        string GetAbsolutePath(string path);
        string GetRelativePath(string path);


        void ShowFolder(string relativePath);
        string OpenFolderPicker();
        string CreateFolderPath(string? parentFolderPath = null, string? title = null);
        void CreateFolder(string path);
        void DeleteFolder(string path, bool recursive = true);
        void CopyFolderTo(string sourceDirectory, string targetDirectory);

        void OpenFile(string relativePath);
        string OpenFilePicker(string? extension = null);
        string CreateFilePath(string parentFolderPath, string extension, string? title = null);
        void DeleteFile(string path);
        string CopyFileToTmp(string sourceFile);
        void CopyFileToNewFile(string sourceFile, string targetFile);
        void CopyFileToClipboard(string relativePath);

        IEnumerable<string> OpenMultiselectPicker(bool folderPickerMode = false);
    }
}
