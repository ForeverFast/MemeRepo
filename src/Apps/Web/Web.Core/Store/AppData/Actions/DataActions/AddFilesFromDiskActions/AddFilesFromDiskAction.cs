namespace Web.Core.Store.AppData.Actions.DataActions.AddFilesFromDiskActions
{
    internal record AddFilesFromDiskAction
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
