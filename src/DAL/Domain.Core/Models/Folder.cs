namespace Domain.Core.Models
{
    [Table("Folders")]
    public class Folder : FolderObject
    {
        public List<Folder> Folders { get; set; } = new List<Folder>();

        public List<Meme> Memes { get; set; } = new List<Meme>();
    }
}
