namespace Web.Core.Store.App.Actions.FolderActions.DeleteFolderActions
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
