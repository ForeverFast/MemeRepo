namespace Business.Core.Dtos
{
    public record AppDataDto
    {
        public HashSet<FolderDto> Folders { get; init; } = new HashSet<FolderDto>();
        public HashSet<TagDto> Tags { get; init; } = new HashSet<TagDto>();
    }
}
