using Web.Core.Base.Store.States;
using Web.Core.Models;
using Web.Core.Models.Components;

namespace Web.Core.Store.AppData
{
    public record AppDataState : BaseState
    {
        public FolderTreeViewModel? CurrentFolder { get; init; }

        public HashSet<FolderTreeViewModel> Folders { get; init; } = new HashSet<FolderTreeViewModel>();
        public List<TagViewModel> Tags { get; init; } = new List<TagViewModel>();
    }
}
