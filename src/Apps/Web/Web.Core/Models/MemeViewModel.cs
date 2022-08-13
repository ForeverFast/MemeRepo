namespace Web.Core.Models
{
    public record MemeViewModel
    {
        public Guid Id { get; init; }
        public Guid? ParentFolderId { get; init; }

        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Path { get; init; } = string.Empty;
        public uint Position { get; init; }

        public FolderViewModel? ParentFolder { get; set; }
        public HashSet<TagViewModel> Tags { get; set; } = new HashSet<TagViewModel>();
    }
}
