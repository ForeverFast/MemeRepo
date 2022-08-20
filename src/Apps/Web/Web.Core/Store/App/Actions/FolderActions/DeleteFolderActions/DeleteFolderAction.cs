namespace Web.Core.Store.App.Actions.FolderActions.DeleteFolderActions
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
