using Web.Core.Models;

namespace Web.Core.Store.AppData.Actions.FolderActions.CreateFolderActions
{
    internal record CreateFolderAction
    {
        public Guid? ParentFolderId { get; init; }

        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public uint Position { get; init; }

        public List<TagViewModel> Tags { get; set; } = new();
    }
}
