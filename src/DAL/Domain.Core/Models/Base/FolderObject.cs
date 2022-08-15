namespace Domain.Core.Models.Base
{
    public abstract class FolderObject : ObservableEntity
    {
        public Guid? ParentFolderId { get; set; }


        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty; 
        public uint Position { get; set; }

        [Association(ThisKey = nameof(ParentFolderId), OtherKey = nameof(Folder.Id))]
        public Folder? ParentFolder { get; set; }
        [Association(ThisKey = nameof(Id), OtherKey = nameof(Folder.ParentFolderId))]
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public override string ToString() => this.Title;
    }
}
