using Web.Core.Models.Components;

namespace Web.Core.Store.AppData.Actions.FolderActions.UpdateFolderActions
{
    internal record UpdateFolderSuccessAction : BaseSuccessAction
    {
        public UpdateFolderSuccessAction(FolderTreeViewModel updatedFolder)
        {
            UpdatedFolder = updatedFolder;
        }

        public FolderTreeViewModel UpdatedFolder { get; }
    }
}
