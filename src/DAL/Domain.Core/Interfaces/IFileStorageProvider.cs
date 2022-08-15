namespace Domain.Core.Interfaces
{
    public interface IFileStorageProvider
    {
        string RootPath { get; }
        string TmpPath { get; }

        string GetAbsolutePath(string path);
        string GetRelativePath(string path);
        void CreateFolder(string path);
        string CreateFolderPath(string? parentFolderPath = null, string? title = null);
        string CreateFilePath(string parentFolderPath, string extension, string? title = null);
        void DeleteFolder(string path, bool recursive = true);
        void DeleteFile(string path);

        string CopyFileToTmp(string sourceFile);
        void CopyFileToNewFile(string sourceFile, string targetFile);
        void CopyDirectoryTo(string sourceDirectory, string targetDirectory);
    }
}
