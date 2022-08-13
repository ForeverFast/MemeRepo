namespace Domain.Core.Models.Base
{
    public abstract class FolderObject : ObservableEntity
    {
        public Guid? ParentFolderId { get; set; }


        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty; 
        public uint Position { get; set; }


        public Folder? ParentFolder { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public override string ToString() => this.Title;
    }
}
