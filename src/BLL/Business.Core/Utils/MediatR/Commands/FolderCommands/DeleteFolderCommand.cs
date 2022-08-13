
namespace Business.Core.Utils.MediatR.Commands.FolderCommands
{
    public record DeleteFolderCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
    }
}
