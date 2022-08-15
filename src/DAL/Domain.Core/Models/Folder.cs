namespace Domain.Core.Models
{
    [Table("Folders")]
    public class Folder : FolderObject
    {
        [Association(ThisKey = nameof(Id), OtherKey = nameof(Folder.ParentFolderId))]
        public List<Folder> Folders { get; set; } = new List<Folder>();

        [Association(ThisKey = nameof(Id), OtherKey = nameof(Meme.ParentFolderId))]
        public List<Meme> Memes { get; set; } = new List<Meme>();
    }
}
