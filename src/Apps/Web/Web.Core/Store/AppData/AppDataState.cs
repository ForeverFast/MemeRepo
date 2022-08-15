using Web.Core.Base.Store.States;
using Web.Core.Models;
using Web.Core.Models.Components;

namespace Web.Core.Store.AppData
{
    internal record AppDataState : BaseState
    {
        public FolderTreeViewModel? CurrentFolder { get; init; }

        public HashSet<FolderTreeViewModel> Folders { get; init; } = new();
        public List<TagViewModel> Tags { get; init; } = new();

        public IEnumerable<FolderTreeViewModel> FolderList => Folders.SelectManyRecursive(x => x.Folders);
        public string GetFolderRelativePath(Guid? folderId)
        {
            if (!folderId.HasValue)
                return string.Empty;

            var pathParts = FolderList.First(x => x.Id == folderId)
                .SelectRecursive(x => x.ParentFolder)
                .Reverse()
                .Select(x => x.Path)
                .ToArray();

            return Path.Combine(pathParts);
        }


        public string GetFileRelativePath(Guid parentFolderId, string filePath)
            => Path.Combine("user-files", GetFolderRelativePath(parentFolderId), filePath);
    }
}
