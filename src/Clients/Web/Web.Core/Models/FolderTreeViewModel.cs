namespace Web.Core.Models
{
    public class FolderTreeViewModel
    {
        public Guid Id { get; set; }
        public Guid? ParentFolderId { get; set; }
        public string Title { get; set; }
        public HashSet<FolderTreeViewModel> InnerFolders { get; set; }



        public bool IsExpanded { get; set; }
        public bool IsSelected { get; set; }
        public bool IsActivated { get; set; }
        public bool IsChecked { get; set; } = false;


        public bool HasChild => InnerFolders != null && InnerFolders.Count > 0;
    }
}
