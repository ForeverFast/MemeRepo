namespace Web.Core.Store.AppData.Actions.MemeActions.CreateMemeActions
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
