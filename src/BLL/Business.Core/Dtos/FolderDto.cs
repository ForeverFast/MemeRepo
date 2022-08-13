namespace Business.Core.Dtos
{
    public record FolderDto
    {
        public Guid Id { get; init; }
        public Guid? ParentFolderId { get; init; }

        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Path { get; init; } = string.Empty;
        public uint Position { get; init; }

        public List<FolderDto> Folders { get; init; } = new List<FolderDto>();
        public List<MemeDto> Memes { get; init; } = new List<MemeDto>();
        public List<TagDto> Tags { get; init; } = new List<TagDto>();
    }
}
