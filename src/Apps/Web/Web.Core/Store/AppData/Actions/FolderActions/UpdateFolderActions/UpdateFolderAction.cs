namespace Web.Core.Store.AppData.Actions.FolderActions.UpdateFolderActions
{
    internal record UpdateFolderAction
    {
        public UpdateFolderAction(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
