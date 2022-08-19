using Web.Core.Models;
using Web.Core.Models.Components;

namespace Web.Core.Store.AppData.Actions.DataActions.AddFilesFromDiskActions
{
    internal record AddFilesFromDiskSuccessAction : BaseSuccessAction
    {
        public AddFilesFromDiskSuccessAction(Guid? parentFolderId,
                                             HashSet<FolderTreeViewModel> newFolders,
                                             List<MemeViewModel> newMemes)
        {
            ParentFolderId = parentFolderId;
            NewFolders = newFolders;
            NewMemes = newMemes;
        }

        public Guid? ParentFolderId { get; }
        public HashSet<FolderTreeViewModel> NewFolders { get; }
        public List<MemeViewModel> NewMemes { get; }
    }
}
