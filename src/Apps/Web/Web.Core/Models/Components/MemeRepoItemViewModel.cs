using Web.Core.Enums.Components;

namespace Web.Core.Models.Components
{
    public class MemeRepoItemViewModel
    {
        public Guid Id { get; set; }
        public Guid? ParentFolderId { get; set; }
        public string Title { get; set; } = "Untitled";
        public string Path { get; set; } = string.Empty;
        public MemeRepoItemType FolderObjectType { get; set; }

        public bool IsSelected { get; set; }
    }
}
