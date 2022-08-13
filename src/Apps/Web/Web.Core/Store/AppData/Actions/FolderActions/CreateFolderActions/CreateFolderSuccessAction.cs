using Web.Core.Base.Store.Actions;
using Web.Core.Models.Components;

namespace Web.Core.Store.AppData.Actions.FolderActions.CreateFolderActions
{
    internal record CreateFolderSuccessAction : BaseSuccessAction
    {
        public CreateFolderSuccessAction(FolderTreeViewModel newFolder)
        {
            NewFolder = newFolder;
        }

        public FolderTreeViewModel NewFolder { get; }
    }
}
