namespace Web.Core.Store.AppData.Actions.TagActions.CreateTagActions
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
