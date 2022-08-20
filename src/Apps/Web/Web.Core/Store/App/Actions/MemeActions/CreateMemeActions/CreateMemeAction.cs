namespace Web.Core.Store.App.Actions.MemeActions.CreateMemeActions
{
    internal record CreateMemeAction
    {
        public CreateMemeAction(Guid? parentFolderId)
        {
            ParentFolderId = parentFolderId;
        }

        public Guid? ParentFolderId { get; }
    }
}
