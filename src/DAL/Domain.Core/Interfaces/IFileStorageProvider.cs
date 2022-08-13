namespace Domain.Core.Interfaces
{
    public interface IFileStorageProvider
    {
        string RootPath { get; }

        void CreateFolder(string path);
        void DeleteFolder(string path, bool recursive = true);

        void CopyDirectoryTo(string sourceDirectory, string targetDirectory);
    }
}
