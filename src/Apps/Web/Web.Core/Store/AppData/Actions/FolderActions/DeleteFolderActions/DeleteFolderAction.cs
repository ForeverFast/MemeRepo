namespace Web.Core.Store.AppData.Actions.FolderActions.DeleteFolderActions
{
    internal record DeleteFolderAction
    {
        public DeleteFolderAction(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
