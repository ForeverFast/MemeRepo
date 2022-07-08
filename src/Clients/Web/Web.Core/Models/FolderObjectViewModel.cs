namespace Web.Core.Models
{
    public class FolderObjectViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public FolderObjectType FolderObjectType { get; set; }
    }

    public enum FolderObjectType
    {
        Folder,
        Img
    }
}
