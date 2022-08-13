namespace Business.Core.Utils.MediatR.Commands.FolderCommands
{
    public record UpdateFolderCommand : IRequest<FolderDto>
    {
        public Guid Id { get; init; }
        public Guid? ParentFolderId { get; init; }

        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Path { get; init; } = string.Empty;
        public uint Position { get; init; }
    }
}
