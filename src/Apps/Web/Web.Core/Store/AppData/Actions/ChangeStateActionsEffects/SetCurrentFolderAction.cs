using Web.Core.Models.Components;

namespace Web.Core.Store.AppData.Actions.ChangeStateActionsEffects
{
    public record SetCurrentFolderAction
    {
        public FolderTreeViewModel? Folder { get; init; }
    }
}
