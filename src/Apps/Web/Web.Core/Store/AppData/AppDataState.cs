using System.Linq;
using Web.Core.Base.Store.States;
using Web.Core.Enums;
using Web.Core.Models;
using Web.Core.Models.Components;
using Web.Core.Models.Components.Search;

namespace Web.Core.Store.AppData
{
    internal record AppDataState : BaseState
    {
        #region Current Content

        public ContentType CurrentContentType { get; init; }
        public Guid? CurrentContentId { get; init; }
        public List<MemeRepoItemViewModel> Items { get; init; } = new();

        public HashSet<FolderTreeViewModel> FolderPath { get; init; } = new();
        public FilterModel? Filter { get; init; } 

        #endregion

        public HashSet<FolderTreeViewModel> Folders { get; init; } = new();
        public List<TagViewModel> Tags { get; init; } = new();

        public IEnumerable<FolderTreeViewModel> FolderList => Folders.SelectManyRecursive(x => x.Folders);
        public string GetFolderRelativePath(Guid? folderId)
        {
            if (!folderId.HasValue)
                return string.Empty;

            var pathParts = new List<string> { "user-files" };

            pathParts
                .AddRange(FolderList.First(x => x.Id == folderId)
                .SelectRecursive(x => x.ParentFolder)
                .Reverse()
                .Select(x => x.Path));

            return Path.Combine(pathParts.ToArray());
        }


        public string GetFileRelativePath(Guid? parentFolderId, string filePath)
            => Path.Combine(GetFolderRelativePath(parentFolderId), filePath);


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
