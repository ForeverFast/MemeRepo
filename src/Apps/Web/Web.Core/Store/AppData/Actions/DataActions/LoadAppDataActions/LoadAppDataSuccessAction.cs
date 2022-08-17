using Web.Core.Models;
using Web.Core.Models.Components;

namespace Web.Core.Store.AppData.Actions.DataActions.LoadAppDataActions
{
    internal record LoadAppDataSuccessAction
    {
        public HashSet<FolderTreeViewModel> Folders { get; init; } = new HashSet<FolderTreeViewModel>();
        public List<TagViewModel> Tags { get; init; } = new List<TagViewModel>();
    }
}
