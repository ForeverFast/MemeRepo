namespace Business.Core.Dtos
{
    public record MemeDto
    {
        public Guid Id { get; init; }
        public Guid? ParentFolderId { get; init; }

        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Path { get; init; } = string.Empty;
        public uint Position { get; init; }

        public List<TagDto> Tags { get; init; } = new List<TagDto>();
    }
}
