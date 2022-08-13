namespace Web.Core.Store.AppData.Actions.FolderActions.DeleteFolderActions
{
    internal record DeleteFolderAction
    {
        public DeleteFolderAction(Guid folderId)
        {
            FolderId = folderId;
        }

        public Guid FolderId { get; init; }
    }
}
