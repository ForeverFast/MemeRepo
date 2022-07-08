namespace Domain.Core.Models.Base
{
    public abstract class FolderObject : ObservableEntity
    {
        public Guid? ParentFolderId { get; set; }


        public string Title { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public uint Position { get; set; }


        public Folder ParentFolder { get; set; }
        public List<Tag> Tags { get; set; }

        public override string ToString() => this.Title;
    }
}
