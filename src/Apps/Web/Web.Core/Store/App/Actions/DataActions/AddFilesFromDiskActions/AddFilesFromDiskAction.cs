namespace Web.Core.Store.App.Actions.DataActions.AddFilesFromDiskActions
{
    public record AddFilesFromDiskAction
    {
        public AddFilesFromDiskAction(Guid? parentFolderId, bool dialogFlag = true)
        {
            ParentFolderId = parentFolderId;
            DialogFlag = dialogFlag;
        }

        public Guid? ParentFolderId { get; }
        public bool DialogFlag { get; }
        public List<string>? FilePaths { get; init; }
    }
}
