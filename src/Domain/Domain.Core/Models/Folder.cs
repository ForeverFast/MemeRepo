namespace Domain.Core.Models
{
    public class Folder : FolderObject
    {
        public List<Folder> Folders { get; set; }

        public List<Meme> Memes { get; set; }
    }
}
