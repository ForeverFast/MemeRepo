namespace Web.Core.Store.AppData.Actions.FolderActions.CreateFolderActions
{
    internal record CreateFolderAction
    {
        public CreateFolderAction(Guid? parentFolderId)
        {
            ParentFolderId = parentFolderId;
        }

        public Guid? ParentFolderId { get; init; }
    }
}
