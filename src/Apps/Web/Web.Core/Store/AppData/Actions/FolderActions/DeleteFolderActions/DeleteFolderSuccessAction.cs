using Web.Core.Base.Store.Actions;

namespace Web.Core.Store.AppData.Actions.FolderActions.DeleteFolderActions
{
    internal record DeleteFolderSuccessAction : BaseSuccessAction
    {
        public DeleteFolderSuccessAction(Guid deletedFolderId)
        {
            DeletedFolderId = deletedFolderId;
        }

        public Guid DeletedFolderId { get; }
    }
}
