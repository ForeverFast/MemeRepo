namespace Web.Core.Models.Components
{
    public class FolderTreeViewModel
    {
        public Guid Id { get; set; }
        public Guid? ParentFolderId { get; set; }

        public FolderTreeViewModel? ParentFolder { get; set; }
        public HashSet<FolderTreeViewModel> Folders { get; set; } = new();

        public string Title { get; set; } = string.Empty;

        public bool IsExpanded { get; set; }
        public bool PathFlag { get; set; } = false;

        public bool HasChild => Folders != null && Folders.Count > 0;
    }
}
