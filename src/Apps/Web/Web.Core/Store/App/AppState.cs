using Domain.Core.Enums;
using Web.Core.Models;
using Web.Core.Models.Components;
using Web.Core.Models.Components.Search;

namespace Web.Core.Store.App
{
    internal record AppState : BaseState
    {
        #region Current Content

        public ContentType CurrentContentType { get; init; }
        public Guid? CurrentContentId { get; init; }
        public List<MemeRepoItemViewModel> Items { get; init; } = new();

        public HashSet<FolderTreeViewModel> FolderPath { get; init; } = new();
        public FilterModel? Filter { get; init; }

        #endregion

        #region Main Content

        public HashSet<FolderTreeViewModel> Folders { get; init; } = new();
        public List<TagViewModel> Tags { get; init; } = new();

        #endregion

        public AppWindowState AppWindowState { get; init; }
        public bool ShowFileDropBlock { get; init; }

        public IEnumerable<FolderTreeViewModel> FolderList => Folders.SelectManyRecursive(x => x.Folders);

        public string GetFolderRelativePath(Guid? folderId)
        {
            var pathParts = new List<string> { "user-files" };

            if (folderId.HasValue)
                pathParts
                    .AddRange(FolderList.First(x => x.Id == folderId)
                    .SelectRecursive(x => x.ParentFolder)
                    .Reverse()
                    .Select(x => x.Path));

            return Path.Combine(pathParts.ToArray());
        }


        public string GetFileRelativePath(Guid? parentFolderId, string filePath)
            => Path.Combine(GetFolderRelativePath(parentFolderId), filePath);

        public string GetFileFolderRelativePath(Guid id)
        {
            var targetItem = Items.First(x => x.Id == id);
            return this.GetFolderRelativePath(targetItem.ParentFolderId);
        }

        public bool Validate(MemeRepoItemViewModel item, List<Guid> tags)
        {
            if (Filter != null)
            {
                var tagIds = Tags.Select(x => x.Id);

                bool titleCheck = !string.IsNullOrEmpty(Filter.SearchTitle) ? item.Title.Contains(Filter.SearchTitle) : true;
                bool tagCheck = Tags.Count > 0 ? tags.All(x => tagIds.Contains(x)) : true;

                bool result = titleCheck && tagCheck;

                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
